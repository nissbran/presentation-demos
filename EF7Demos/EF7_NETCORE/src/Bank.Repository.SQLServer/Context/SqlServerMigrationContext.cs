namespace Bank.Repository.SQLServer.Context
{
    using Bank.Repository.Context;
    using Microsoft.Data.Entity.Infrastructure;

    public class SqlServerMigrationContext : BankContext
    {
        public SqlServerMigrationContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
