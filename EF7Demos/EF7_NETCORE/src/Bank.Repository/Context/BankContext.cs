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

            optionsBuilder.UseSqlServer(
                "Server = (localdb)\\MSSQLLocalDB; Initial Catalog = EF7Context; Integrated Security = true; ");
            //optionsBuilder.UseSqlServer(
            //     ConfigurationManager.ConnectionStrings["EFMigrationDb"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSqlServerSequenceHiLo();

            EntityMappingConfiguration.EntityMappings.ForEach(map => map.ConfigureModel(modelBuilder));
        }
    }
}
