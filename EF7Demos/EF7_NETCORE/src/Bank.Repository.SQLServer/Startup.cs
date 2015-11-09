namespace Bank.Repository.SQLServer
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
            var connectionString =
                @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = EF7CoreContext2; Integrated Security = True;";

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<SqlServerMigrationContext>(builder => builder.UseSqlServer(connectionString));
        }
    }
}
