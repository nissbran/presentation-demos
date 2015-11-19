namespace Bank.Repository.SqlServer.Context
{
    using Microsoft.Data.Entity;
    using Microsoft.Data.Entity.Infrastructure;
    using Repository.Context;

    public class SqlServerMigrationContext : BankContext
    {
        public SqlServerMigrationContext()
        {

        }

        public SqlServerMigrationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ForSqlServerUseSequenceHiLo();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\MSSQLLocalDB;Initial Catalog=EF7Context;Integrated Security=true;");
        }
    }
}
