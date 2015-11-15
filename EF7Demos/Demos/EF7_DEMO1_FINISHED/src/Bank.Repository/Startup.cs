using Bank.Repository.Context;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;

namespace Bank.Repository
{
    public class Startup
    {
        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                    .AddSqlServer()
                    .AddDbContext<BankContext>(builder => builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MigrationContext; Integrated Security = True;"));
        }

        public void Configure()
        {
            
        }
    }
}