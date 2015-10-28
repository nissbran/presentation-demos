namespace Bank.Repository.SQLite.Context
{
    using Domain.Models;
    using Domain.Models.Customers;
    using Mapping;
    using Microsoft.Data.Entity;

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
