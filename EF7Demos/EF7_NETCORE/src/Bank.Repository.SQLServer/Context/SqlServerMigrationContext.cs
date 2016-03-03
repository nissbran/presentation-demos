namespace Bank.Repository.SQLServer.Context
{
    using Bank.Repository.Context;
    using Domain.Models.Customers;
    using Microsoft.Data.Entity;
    using Microsoft.Data.Entity.Infrastructure;

    public class SqlServerMigrationContext : BankContext
    {
        public SqlServerMigrationContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence("Test").IncrementsBy(10);

            // modelBuilder.ForSqlServerUseSequenceHiLo("HiLoSequence");

            modelBuilder.Entity<BankCustomer>()
                        .Property(e => e.CustomerId)
                        .HasDefaultValueSql("next value for Test")
                        .Metadata.IsReadOnlyBeforeSave = true;

            base.OnModelCreating(modelBuilder);
        }
    }
}
