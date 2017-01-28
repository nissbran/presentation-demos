namespace Demo.Bank.WebApi.Configuration
{
    using System.Linq;
    using System.Reflection;
    using Cache;
    using Domain.Commands;
    using Domain.Commands.Account;
    using EventStore.Lib.Common;
    using EventStore.Lib.Common.Configurations;
    using EventStore.Lib.Common.Domain;
    using EventStore.Lib.Write.Persistance;
    using Logger;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;

    public static class ServiceCollectionExtensions
    {
        private static readonly Assembly DomainAssembly = typeof(CreateAccountCommand).GetTypeInfo().Assembly;

        public static void AddCommandHandlers(this IServiceCollection services)
        {
            services.AddSingleton<ICommandMediator, CommandMediator>();

            var exportedTypes = DomainAssembly.GetExportedTypes().ToList();
            var commandHandlers = exportedTypes.Where(t => typeof(ICommandHandler).IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract).ToList();

            foreach (var commandHandlerType in commandHandlers)
            {
                services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(ICommandHandler), commandHandlerType));
            }
        }

        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<IDomainRepository, EventStoreDomainRepository>(provider =>
            {
                var connection = EventStoreConnectionFactory.Create(
                    new EventStore3NodeClusterConfiguration(), 
                    new EventStoreLogger(provider.GetRequiredService<ILoggerFactory>()),
                    "admin", "changeit");

                connection.ConnectAsync().Wait();

                return new EventStoreDomainRepository(connection);
            });

            services.AddSingleton<IRedisRepository, RedisRepository>();
        }
    }
}