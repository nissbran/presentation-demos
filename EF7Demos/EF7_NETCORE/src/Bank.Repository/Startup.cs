namespace Bank.Repository
{
    using Context;
    using Microsoft.AspNet.Builder;
    using Microsoft.Data.Entity;
    using Microsoft.Framework.DependencyInjection;

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
        }

        // This method gets called by the runtime.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = @"Server = (localdb)\MSSQLLocalDB; Initial Catalog = EF7Context; Integrated Security = true;";

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<BankContext>(options => options.UseSqlServer(connection));
        }
    }
}
