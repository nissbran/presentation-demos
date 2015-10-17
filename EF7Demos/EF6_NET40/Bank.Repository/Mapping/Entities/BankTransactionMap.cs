namespace Bank.Repository.Mapping.Entities
{
    using System.Data.Entity;
    using Domain.Models;
    using Interfaces;

    public class BankTransactionMap : IEntityMap
    {
        public void ConfigureModel(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BankTransaction>()
                .HasKey(customer => customer.BankTransactionId);

            modelBuilder.Entity<BankTransaction>()
                .HasRequired(t => t.Customer)
                .WithMany()
                .HasForeignKey(customer => customer.CustomerId);
        }
    }
}
