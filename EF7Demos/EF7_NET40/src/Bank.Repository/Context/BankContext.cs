using Bank.Domain.Models;
using Bank.Repository.Mapping;
using Microsoft.Data.Entity;

namespace Bank.Repository.Context
{
    using Domain.Models.Customer;
    using Microsoft.Data.Entity.Infrastructure;

    public class BankContext : DbContext
    {
        public DbSet<BankCustomer> Customers { get; set; }
        public DbSet<BankTransaction> Transactions { get; set; }

        public BankContext()
        {
            
        }

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
