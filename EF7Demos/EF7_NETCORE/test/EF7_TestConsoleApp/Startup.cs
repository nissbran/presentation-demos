namespace EF7_TestConsoleApp
{
    using Bank.Repository.Context;
    using Microsoft.AspNet.Builder;
    using Microsoft.Data.Entity;
    using Microsoft.Framework.DependencyInjection;

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
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
