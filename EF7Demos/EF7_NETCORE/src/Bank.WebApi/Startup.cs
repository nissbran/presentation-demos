using System;

namespace Bank.WebApi
{
    using Bank.Domain.Models;
    using Bank.Domain.Models.Customers;
    using Bank.Repository.Context;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting;
    using Microsoft.Data.Entity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.PlatformAbstractions;
    using Repository.SQLite.Context;
    using Repository.SQLServer.Context;

    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public bool UseSqlite { get; private set; }

        public Startup(IApplicationEnvironment applicationEnvironment)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(applicationEnvironment.ApplicationBasePath)
                .AddJsonFile("config.json");

            Configuration = configuration.Build();
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            UseSqlite = bool.Parse(Configuration["Data:UseSqlite"]);

            var efBuilder = services.AddEntityFramework();

            if (UseSqlite)
            {
                var sqliteOptions = $"Filename={Configuration["Data:Sqlite:Filename"]}";

                efBuilder
                    .AddSqlite()
                    .AddDbContext<BankContext>(builder => builder.UseSqlite(sqliteOptions))
                    .AddDbContext<SqliteMigrationContext>(builder => builder.UseSqlite(sqliteOptions));
            }
            else
            {
                var sqlServerOptions = Configuration["Data:SqlServer:ConnectionString"];

                efBuilder
                    .AddSqlServer()
                    .AddDbContext<BankContext>(builder => builder.UseSqlServer(sqlServerOptions))
                    .AddDbContext<SqlServerMigrationContext>(builder => builder.UseSqlServer(sqlServerOptions));
            }

        }

        // Configure is called after ConfigureServices is called.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider)
        {
            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            // Add the platform handler to the request pipeline.
            app.UseIISPlatformHandler();

            // Configure the HTTP request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc();

            var migrationContext = UseSqlite
                ? (BankContext)serviceProvider.GetService<SqliteMigrationContext>()
                : (BankContext)serviceProvider.GetService<SqlServerMigrationContext>();

            ResetAndMigrateDatabase(migrationContext);
        }

        private void ResetAndMigrateDatabase(BankContext bankContext)
        {
            bankContext.Database.EnsureDeleted();
            bankContext.Database.Migrate();

            var customer = new PrivatePerson("Sql Nisse", "Nisse");
            bankContext.Customers.Add(customer);
            bankContext.Transactions.Add(new BankTransaction(customer, 44));

            bankContext.SaveChanges();
        }
    }
}
