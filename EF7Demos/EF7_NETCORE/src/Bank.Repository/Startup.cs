namespace Bank.Repository
{
    using Context;
    using Microsoft.AspNet.Builder;
    using Microsoft.Framework.DependencyInjection;

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
