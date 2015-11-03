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

    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        
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

            if (bool.Parse(Configuration["Data:UseSqlite"]))
            {
                services.AddEntityFramework()
                        .AddSqlite()
                        .AddDbContext<BankContext>(builder => builder.UseSqlite($"Filename={Configuration["Data:Sqlite:Filename"]}"));
            }
            else
            {
                services.AddEntityFramework()
                        .AddSqlServer()
                        .AddDbContext<BankContext>(builder => builder.UseSqlServer(Configuration["Data:SqlServer:ConnectionString"]));
            }
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            BankContext context)
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

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var customer = new PrivatePerson("Nisse", "Nisse");
            context.Customers.Add(customer);
            context.Transactions.Add(new BankTransaction(customer, 44));

            context.SaveChanges();
        }
    }
}
