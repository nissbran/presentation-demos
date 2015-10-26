namespace Bank.Repository.Mapping.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using Domain.Models;
    using Interfaces;

    public class BankTransactionMap : IEntityMap
    {
        public void ConfigureModel(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BankTransaction>()
                .Property(transaction => transaction.BankTransactionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder
                .Entity<BankTransaction>()
                .HasKey(transaction => transaction.BankTransactionId);

            modelBuilder.Entity<BankTransaction>()
                .HasRequired(t => t.Customer)
                .WithMany()
                .HasForeignKey(transaction => transaction.CustomerId);
        }
    }
}
