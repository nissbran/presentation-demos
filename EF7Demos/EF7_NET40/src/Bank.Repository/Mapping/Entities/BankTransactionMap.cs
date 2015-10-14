namespace Bank.Repository.Mapping.Entities
{
    using System;
    using Domain.Models;
    using Interfaces;
    using Microsoft.Data.Entity;

    public class BankTransactionMap : IEntityMap
    {
        public void ConfigureModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BankTransaction>()
                .Key(customer => customer.BankTransactionId);
            
            modelBuilder.Entity<BankTransaction>().Property<long>("CustomerId");

            modelBuilder
                .Entity<BankTransaction>()
                .Reference(transaction => transaction.Customer)
                .InverseCollection()
                .ForeignKey("CustomerId")
                .Required();
        }
    }
}
