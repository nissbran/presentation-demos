namespace Bank.WebApi
{
    using Bank.Domain.Models;
    using Bank.Domain.Models.Customers;
    using Bank.Repository.Context;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting;
    using Microsoft.Data.Entity;
    using Microsoft.Dnx.Runtime;
    using Microsoft.Framework.Configuration;
    using Microsoft.Framework.DependencyInjection;
    using Microsoft.Framework.Logging;
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

            var entityFrameworkBuilder = services.AddEntityFramework()
                .AddSqlite();
                                                 //.AddSqlServer();
            if (UseSqlite)
            {
                entityFrameworkBuilder.AddDbContext<BankContext>(builder => builder.UseSqlite($"Filename={Configuration["Data:Sqlite:Filename"]}"));
            }
            else
            {
                entityFrameworkBuilder.AddDbContext<BankContext>(builder => builder.UseSqlServer(Configuration["Data:SqlServer:ConnectionString"]));
            }

            entityFrameworkBuilder
                .AddDbContext<SqliteMigrationContext>(builder => builder.UseSqlite($"Filename={Configuration["Data:Sqlite:Filename"]}"));
            //.AddDbContext<SqlServerMigrationContext>(builder => builder.UseSqlServer(Configuration["Data:SqlServer:ConnectionString"]));
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            //SqlServerMigrationContext sqlContext,
            SqliteMigrationContext sqlliteContext)
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

            if (UseSqlite)
            {
                sqlliteContext.Database.EnsureDeleted();
                sqlliteContext.Database.Migrate();

                var customer = new PrivatePerson("Sqlite Nisse", "Nisse");
                sqlliteContext.Customers.Add(customer);
                sqlliteContext.Transactions.Add(new BankTransaction(customer, 33));
            }
            else
            {
                //sqlContext.Database.EnsureDeleted();
                //sqlContext.Database.Migrate();

                //var customer = new PrivatePerson("SqlServer Nisse", "Nisse");
                //sqlContext.Customers.Add(customer);
                //sqlContext.Transactions.Add(new BankTransaction(customer, 44));

                //sqlContext.SaveChanges();
            }
        }
    }
}
