namespace Demo.Bank.ReadStoreSync.Handlers
{
    using System;
    using System.Collections.Concurrent;
    using EventStore.Lib.Common.Domain;
    using Repositories;

    public class AccountInformationHandler
    {
        private readonly AccountInformationRepository _accountInformationRepository;
        public AccountInformationHandler(AccountInformationRepository accountInformationRepository)
        {
            _accountInformationRepository = accountInformationRepository;
        }

        public void UpdateReadModel(string accountNumber, IDomainEvent domainEvent, int version, long position)
        {
            var accountBalance = $"balance-{accountNumber}";

            _accountInformationRepository.UpdateModel(accountNumber, domainEvent, version, position);
        }
    }
}