namespace Demo.Bank.WebApi
{
    using Configuration;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using MySql.Data.MySqlClient;
    using MySQL.Data.Entity.Extensions;
    using Sql.Context;

    public class Startup
    {
        private const string MySqlConnectionString = "server=localhost;userid=root;password=123456;port=3306;";
        private const string DataBase = "accountinfo";

        public void ConfigureServices(IServiceCollection services)
        {
            MySqlConnection connection = new MySqlConnection
            {
                ConnectionString = MySqlConnectionString
            };
            connection.Open();

            MySqlCommand command = new MySqlCommand("CREATE DATABASE IF NOT EXISTS accountinfo", connection);

            command.ExecuteNonQuery();
            connection.Close();

            services.AddDbContext<AccountInformationContext>(options => options.UseMySQL($"{MySqlConnectionString};database={DataBase}"));

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
