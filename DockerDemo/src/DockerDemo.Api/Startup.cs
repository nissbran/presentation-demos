namespace DockerDemo.Api
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Infrastructure.Repository;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);
                // .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            if (env.IsDevelopment())
            {
                Console.WriteLine("--- Current external connection config ---");

                Console.WriteLine("* EventStore");
                Console.WriteLine($"  - Connection: {Configuration["Data:EventStore:ConnectionUri"] ?? "Azure cluster configuration. Check appsettings.json for info."}");
                Console.WriteLine("* SessionCache");
                Console.WriteLine($"  - Connection: {Configuration["Data:SessionCache:ConnectionString"]}");
                Console.WriteLine("* ReadStore");
                Console.WriteLine($"  - Provider: {Configuration["Data:ReadStore:Provider"]}");
                Console.WriteLine($"  - Connection: {Configuration[$"Data:ReadStore:{Configuration["Data:ReadStore:Provider"]}:ConnectionUri"]}");

                Console.WriteLine("------------------------------------------");
            }
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            services.AddSingleton<IEventStore, EventStore>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Information);

            app.UseMvc();
        }
    }
}
