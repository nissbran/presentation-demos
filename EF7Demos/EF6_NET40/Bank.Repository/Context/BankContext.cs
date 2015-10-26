namespace Bank.Repository.Context
{
    using System.Data.Entity;
    using Domain.Models;
    using Domain.Models.Customer;
    using Mapping;

    public class BankContext : DbContext
    {
        public DbSet<BankCustomer> Customers { get; set; } 
        public DbSet<BankTransaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            EntityMappingConfiguration.EntityMappings.ForEach(map => map.ConfigureModel(modelBuilder));
        }
    }
}
