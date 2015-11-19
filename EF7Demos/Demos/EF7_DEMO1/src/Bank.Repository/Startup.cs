namespace Bank.Repository
{
    using Microsoft.Extensions.DependencyInjection;

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
