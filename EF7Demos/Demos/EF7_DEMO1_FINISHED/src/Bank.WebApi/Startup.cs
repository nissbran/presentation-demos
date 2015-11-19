namespace Bank.WebApi
{
    using System;
    using Bank.Domain.Models.Customers;
    using Bank.Repository.Context;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting;
    using Microsoft.Data.Entity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.PlatformAbstractions;

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

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<BankContext>(b => b.UseSqlServer(Configuration["Data:SqlServer:ConnectionString"]));
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider,
            BankContext bankContext)
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

            bankContext.Database.EnsureDeleted();
            bankContext.Database.Migrate();

            bankContext.Customers.Add(new Company("test"));

            bankContext.SaveChanges();
        }
    }
}
