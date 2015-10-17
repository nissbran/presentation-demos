using Bank.Domain.Models;
using Bank.Repository.Mapping;
using Microsoft.Data.Entity;

namespace Bank.Repository.Context
{
    using System.Configuration;
    using Domain.Models.Customer;

    public class BankContext : DbContext
    {
        public DbSet<BankCustomer> Customers { get; set; }
        public DbSet<BankTransaction> Transactions { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                 ConfigurationManager.ConnectionStrings["EFMigrationDb"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSqlServerSequenceHiLo();

            EntityMappingConfiguration.EntityMappings.ForEach(map => map.ConfigureModel(modelBuilder));
        }
    }
}
