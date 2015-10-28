namespace Bank.Repository.SQLite
{
    using Microsoft.AspNet.Builder;
    using Microsoft.Framework.DependencyInjection;
    using SQLite.Context;

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
        }

        // This method gets called by the runtime.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                .AddSqlite()
                .AddDbContext<BankContext>();
        }
    }
}
