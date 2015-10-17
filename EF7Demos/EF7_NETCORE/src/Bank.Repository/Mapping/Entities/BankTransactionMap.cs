namespace Bank.Repository.Mapping.Entities
{
    using Domain.Models;
    using Interfaces;
    using Microsoft.Data.Entity;

    public class BankTransactionMap : IEntityMap
    {
        public void ConfigureModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BankTransaction>()
                .HasKey(customer => customer.BankTransactionId);
            
            modelBuilder.Entity<BankTransaction>().Property<long>("CustomerId");

            modelBuilder
                .Entity<BankTransaction>()
                .HasOne(transaction => transaction.Customer)
                .WithMany()
                .ForeignKey("CustomerId")
                .Required();
        }
    }
}
