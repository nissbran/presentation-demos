namespace Demo.Bank.ReadStoreSync.Handlers
{
    using System;
    using System.Collections.Concurrent;
    using EventStore.Lib.Common.Domain;
    using Repositories;

    public class AccountInformationHandler
    {
        private readonly AccountInformationRepository _accountInformationRepository;
        private readonly ConcurrentDictionary<string, AccountBalanceLock> _accountBalanceLocks = new ConcurrentDictionary<string, AccountBalanceLock>();

        public AccountInformationHandler(AccountInformationRepository accountInformationRepository)
        {
            _accountInformationRepository = accountInformationRepository;
        }

        public void UpdateReadModel(string accountNumber, IDomainEvent domainEvent, int version)
        {
            var accountBalance = $"balance-{accountNumber}";

            var accountBalanceLock = _accountBalanceLocks.GetOrAdd(accountBalance, new AccountBalanceLock(accountBalance));

            // .NET 4.6 only feature, coming in .NET 2.0 Standard
            //lock (string.Intern(accountBalance))
            lock (accountBalanceLock)
            {
                _accountInformationRepository.UpdateModel(accountNumber, domainEvent, version);

                accountBalanceLock.LastAccessed = DateTime.Now;
            }
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