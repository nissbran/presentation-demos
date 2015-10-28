namespace Bank.Repository.SQLServer
{
    using Microsoft.AspNet.Builder;
    using Microsoft.Data.Entity;
    using Microsoft.Framework.DependencyInjection;
    using SQLServer.Context;

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
        }

        // This method gets called by the runtime.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString =
                "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = EF7CoreContext; Integrated Security = True;";

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<BankContext>(builder => builder.UseSqlServer(connectionString));
        }
    }
}
