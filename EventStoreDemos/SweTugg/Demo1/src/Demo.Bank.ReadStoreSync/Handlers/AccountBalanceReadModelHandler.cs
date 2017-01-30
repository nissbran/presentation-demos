namespace Demo.Bank.ReadStoreSync.Handlers
{
    using Domain.Events.Account;
    using Domain.Events.Transactions;
    using Domain.ReadModels;
    using EventStore.Lib.Common.Domain;

    public class AccountBalanceReadModelHandler
    {
        private readonly RedisRepository _redisRepository;

        public AccountBalanceReadModelHandler(RedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }

        public void UpdateReadModel(string accountNumber, IDomainEvent domainEvent)
        {
            var accountBalance = $"balance-{accountNumber}";

            var accountBalanceReadModel = _redisRepository.Get<AccountBalanceReadModel>(accountBalance);

            UpdateReadModel(accountBalanceReadModel, domainEvent);

            _redisRepository.Set(accountBalance, accountBalanceReadModel);
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
    }
}