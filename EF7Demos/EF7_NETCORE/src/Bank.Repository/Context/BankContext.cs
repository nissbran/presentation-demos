using Bank.Domain.Models;
using Microsoft.Data.Entity;

namespace Bank.Repository.Context
{
    using Domain.Models.Customers;
    using Mapping;
    

    public class BankContext : DbContext
    {
        public DbSet<BankCustomer> Customers { get; set; }
        public DbSet<BankTransaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Filename=testdb.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityMappingConfiguration.EntityMappings.ForEach(map => map.ConfigureModel(modelBuilder));
        }
    }
}
