using Bank.Domain.Models;
using Bank.Repository.Mapping;
using Microsoft.Data.Entity;

namespace Bank.Repository.Context
{
    using System.Configuration;

    public class BankContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BankTransaction> Transactions { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                 ConfigurationManager.ConnectionStrings["EFMigrationDb"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSqlServerSequenceHiLo(poolSize: 5);

            EntityMappingConfiguration.EntityMappings.ForEach(map => map.ConfigureModel(modelBuilder));
        }
    }
}
