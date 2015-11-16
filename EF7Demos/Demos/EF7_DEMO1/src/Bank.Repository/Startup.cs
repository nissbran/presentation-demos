namespace Bank.Repository
{
    using Microsoft.Framework.DependencyInjection;

    public class Startup
    {
        private const string MigrationConnectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=MigrationContext; Integrated Security = True;";

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure()
        {
        }
    }
}
