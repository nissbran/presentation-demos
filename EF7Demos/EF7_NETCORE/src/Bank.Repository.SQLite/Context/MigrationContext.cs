namespace Bank.Repository.SQLite.Context
{
    using Bank.Repository.Context;
    using Microsoft.Data.Entity.Infrastructure;

    public class MigrationContext : BankContext
    {
        public MigrationContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
