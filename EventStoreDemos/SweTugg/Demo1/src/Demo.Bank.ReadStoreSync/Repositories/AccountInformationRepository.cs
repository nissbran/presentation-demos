namespace Demo.Bank.ReadStoreSync.Repositories
{
    using System;
    using Domain.Events.Account;
    using Domain.Events.Transactions;
    using EventStore.Lib.Common.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MySql.Data.MySqlClient;
    using MySQL.Data.Entity.Extensions;
    using Sql.Context;
    using Sql.Model;

    public class AccountInformationRepository
    {
        private const string MySqlConnectionString = "server=localhost;userid=root;password=123456;port=3306";
        private const string DataBase = "accountinfo";

        private readonly MySqlConnection _connection;

        private readonly AccountInformationContext _accountContext;

        private readonly IServiceProvider _serviceProvider;

        public AccountInformationRepository()
        {
            _connection = new MySqlConnection
            {
                ConnectionString = MySqlConnectionString
            };
            _connection.Open();
            
            MySqlCommand command = new MySqlCommand("CREATE DATABASE IF NOT EXISTS accountinfo", _connection);
            command.ExecuteNonQuery();           
            //connection.Close();

            _serviceProvider = new ServiceCollection()
              .AddDbContext<AccountInformationContext>(o => o.UseMySQL($"{MySqlConnectionString};database={DataBase}"))
              .BuildServiceProvider();
            
            _accountContext = _serviceProvider.GetRequiredService<AccountInformationContext>();

            _accountContext.Database.EnsureCreated();
        }

        public long GetStartingPosition()
        {
            AccountInformationCheckpoint checkpoint = _accountContext.AccountCheckpoints.FirstOrDefaultAsync(c => c.Id == 1).Result;
            if (checkpoint == null)
            {
                checkpoint = new AccountInformationCheckpoint 
                {
                    Checkpoint = 0
                };
                _accountContext.AccountCheckpoints.Add(checkpoint);
                _accountContext.SaveChanges();   
            }

            return checkpoint.Checkpoint;
        }

        public void UpdateModel(string accountNumber, IDomainEvent domainEvent, int version, long position)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                return;

            AccountInformation account = _accountContext.Accounts.FirstOrDefaultAsync(information => information.AccountNumber == accountNumber).Result;

            if (account == null)
            {
                account = new AccountInformation();

                _accountContext.Accounts.Add(account);
            }

            UpdateInformation(account, domainEvent, version);

            AccountInformationCheckpoint checkout = _accountContext.AccountCheckpoints.FirstOrDefaultAsync(c => c.Id == 1).Result;
            checkout.Checkpoint = position;
            try
            {
                _accountContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private bool UpdateInformation(AccountInformation info, IDomainEvent domainEvent, int version)
        {
            if (info.Version != 0 && info.Version >= version)
            {
                Console.WriteLine("Error");
                return false;
            }

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

            return true;
        }
    }
}
