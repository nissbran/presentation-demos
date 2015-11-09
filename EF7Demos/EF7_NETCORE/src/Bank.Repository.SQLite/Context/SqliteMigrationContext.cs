namespace Bank.Repository.SQLite.Context
{
    using Bank.Repository.Context;
    using Microsoft.Data.Entity.Infrastructure;

    public class SqliteMigrationContext : BankContext
    {
        public SqliteMigrationContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
