namespace Bank.Repository.Context
{
    using Domain.Models;
    using Domain.Models.Customers;
    using Mapping;
    using Microsoft.Data.Entity;
    using Microsoft.Data.Entity.Infrastructure;

    public class BankContext : DbContext
    {
        public DbSet<BankCustomer> Customers { get; set; }
        public DbSet<BankTransaction> Transactions { get; set; }

        public DbSet<CreditCheckResult> CreditCheckResults { get; set; } 

        public BankContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityMappingConfiguration.EntityMappings.ForEach(map => map.ConfigureModel(modelBuilder));
        }
    }
}
