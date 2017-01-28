namespace Demo.Bank.ReadStoreSync.Repositories
{
    using System;
    using Domain.Events.Account;
    using Domain.Events.Transactions;
    using EventStore.Lib.Common.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MySQL.Data.Entity.Extensions;
    using Sql.Context;
    using Sql.Model;

    public class AccountInformationRepository
    {
        private const string MySqlConnectionString = "server=localhost;userid=root;password=123456;port=3306;database=accountinfo";

        private readonly IServiceProvider _serviceProvider;

        public AccountInformationRepository()
        {
            _serviceProvider = new ServiceCollection()
              .AddDbContext<AccountInformationContext>(o => o.UseMySQL(MySqlConnectionString))
              .BuildServiceProvider();

            var dbContext = _serviceProvider.GetRequiredService<AccountInformationContext>();

            dbContext.Database.EnsureCreated();
        }

        public void UpdateModel(string accountNumber, IDomainEvent domainEvent, int version)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                return;

            var context = _serviceProvider.GetRequiredService<AccountInformationContext>();
            
            AccountInformation account = context.Accounts.FirstOrDefaultAsync(information => information.AccountNumber == accountNumber).Result;

            if (account == null)
            {
                account = new AccountInformation();

                context.Accounts.Add(account);
            }

            UpdateInformation(account, domainEvent, version);

            context.SaveChanges();
        }

        private void UpdateInformation(AccountInformation info, IDomainEvent domainEvent, int version)
        {
            if (info.Version != 0 && info.Version >= version)
                return;

            info.Version = version;

            var createdAccount = domainEvent as AccountCreatedEvent;
            if (createdAccount != null)
            {
                info.AccountNumber = createdAccount.AccountNumber;
            }

            var accountContactDetailsEvent = domainEvent as AccountContactDetailsSetEvent;
            if (accountContactDetailsEvent != null)
            {
                info.FirstName = accountContactDetailsEvent.FirstName;
                info.LastName = accountContactDetailsEvent.LastName;
                info.RegistrationNumber = accountContactDetailsEvent.RegistrationNumber;
            }

            var bankTransferEvent = domainEvent as BankTransferTransactionAddedEvent;
            if (bankTransferEvent != null)
            {
                info.Balance += bankTransferEvent.Amount;
            }

            var cardTransactionEvent = domainEvent as CardTransactionAddedEvent;
            if (cardTransactionEvent != null)
            {
                info.Balance += cardTransactionEvent.Amount;
            }
        }
    }
}
