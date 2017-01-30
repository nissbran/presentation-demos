namespace Demo.Bank.ReadStoreSync.Handlers
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Events.Account;
    using Domain.Events.Transactions;
    using Domain.ReadModels;
    using EventStore.Lib.Common.Domain;

    public class AccountBalanceLockingReadModelHandler
    {
        private const int LockExpireTimeInSeconds = 5; 

        private readonly RedisRepository _redisRepository;
        private readonly ConcurrentDictionary<string, AccountBalanceLock> _accountBalanceLocks = new ConcurrentDictionary<string, AccountBalanceLock>();
        private readonly CancellationTokenSource _monitorThreadCancellationTokenSource = new CancellationTokenSource();
        

        public AccountBalanceLockingReadModelHandler(RedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
           // SetupLockingExpireJob();
        }

        public void UpdateReadModel(string accountNumber, IDomainEvent domainEvent, int eventNumber)
        {
            var accountBalance = $"balance-{accountNumber}";

            var accountBalanceLock = _accountBalanceLocks.GetOrAdd(accountBalance, new AccountBalanceLock(accountBalance));

            // .NET 4.6 only feature, coming in .NET 2.0 Standard
            //lock (string.Intern(accountBalance))
            lock (accountBalanceLock)
            {
                var accountBalanceReadModel = _redisRepository.Get<AccountBalanceReadModel>(accountBalance);

                if (accountBalanceReadModel.EventNumbersProcessed.Contains(eventNumber))
                    return;

                accountBalanceReadModel.EventNumbersProcessed.Add(eventNumber);

                UpdateReadModel(accountBalanceReadModel, domainEvent);
                
                _redisRepository.Set(accountBalance, accountBalanceReadModel);

                accountBalanceLock.LastAccessed = DateTime.Now;
            }
        }

        private void UpdateReadModel(AccountBalanceReadModel readModel, IDomainEvent domainEvent)
        {
            var createdAccount = domainEvent as AccountCreatedEvent;
            if (createdAccount != null)
            {
                readModel.AccountNumber = createdAccount.AccountNumber;
            }
            
            var bankTransferEvent = domainEvent as BankTransferTransactionAddedEvent;
            if (bankTransferEvent != null)
            {
                readModel.Balance += bankTransferEvent.Amount;
            }

            var cardTransactionEvent = domainEvent as CardTransactionAddedEvent;
            if (cardTransactionEvent != null)
            {
                readModel.Balance += cardTransactionEvent.Amount;
            }
        }

        private void SetupLockingExpireJob()
        {
            var token = _monitorThreadCancellationTokenSource.Token;
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(new TimeSpan(0, 0, 0, 5), token);

                    var allAccountBalanceLocks = _accountBalanceLocks.Values;
                    var time = DateTime.Now;

                    foreach (var accountBalanceLock in allAccountBalanceLocks)
                    {
                        if (accountBalanceLock.LastAccessed < time.AddSeconds(-LockExpireTimeInSeconds))
                        {
                            AccountBalanceLock removedLock;
                            if (_accountBalanceLocks.TryRemove(accountBalanceLock.Key, out removedLock))
                                Console.WriteLine($"Released balance lock for Key: {removedLock.Key}");
                        }
                    }
                }

            }, token);
        }

        private class AccountBalanceLock
        {
            public string Key { get; }

            public DateTime LastAccessed { get; set; }

            public AccountBalanceLock(string accountNumber)
            {
                Key = accountNumber;
                LastAccessed = DateTime.Now;
            }
        }
    }
}