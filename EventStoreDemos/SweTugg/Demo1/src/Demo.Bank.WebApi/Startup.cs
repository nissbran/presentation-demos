namespace Demo.Bank.WebApi
{
    using Configuration;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using MySQL.Data.Entity.Extensions;
    using Sql.Context;

    public class Startup
    {
        private const string MySqlConnectionString = "server=localhost;userid=root;password=123456;port=3306;database=accountinfo";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AccountInformationContext>(options => options.UseMySQL(MySqlConnectionString));

            services.AddDomainServices();
            services.AddCommandHandlers();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, AccountInformationContext dbContext)
        {
            loggerFactory.AddConsole();

            dbContext.Database.EnsureCreated();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
