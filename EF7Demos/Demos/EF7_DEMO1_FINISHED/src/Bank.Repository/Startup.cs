using Bank.Repository.Context;
using Microsoft.Data.Entity;

namespace Bank.Repository
{
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        private const string MigrationConnectionString =
               "Server=(localdb)\\MSSQLLocalDB;Database=MigrationContext; Integrated Security = True;";
        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                    .AddSqlServer()
                    .AddDbContext<BankContext>(builder => builder.UseSqlServer(MigrationConnectionString));
        }

        public void Configure()
        {
            
        }
    }
}