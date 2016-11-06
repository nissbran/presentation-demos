namespace DockerDemo.Infrastructure.Repository
{
    using System;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Domain;
    using global::EventStore.ClientAPI;
    using Newtonsoft.Json;

    public class EventStore : IEventStore
    {
        private readonly IEventStoreConnection _connection;

        public EventStore()
        {
            var gossipSeeds = new IPEndPoint[] {
                new IPEndPoint(IPAddress.Parse("172.20.0.11"), 2113),
                new IPEndPoint(IPAddress.Parse("172.20.0.12"), 2123),
                new IPEndPoint(IPAddress.Parse("172.20.0.13"), 2133),
            };

            var connectionSettings = ConnectionSettings.Create();
            connectionSettings.SetReconnectionDelayTo(TimeSpan.FromSeconds(1))
                              .KeepReconnecting()
                              .KeepRetrying()
                              .SetMaxDiscoverAttempts(int.MaxValue)
                              .FailOnNoServerResponse()
                              .SetGossipSeedEndPoints(gossipSeeds)
                              .SetGossipTimeout(TimeSpan.FromMilliseconds(1000))
                              .UseConsoleLogger();

            _connection = EventStoreConnection.Create(connectionSettings.Build());
            _connection.ConnectAsync();
        }

        public async Task AddEvent(Event domainEvent)
        {
            domainEvent.MetaData = new DomainMetaData
            {
                EventId = Guid.NewGuid(),
                Created = DateTimeOffset.Now
            };

            var eventData = new EventData(domainEvent.MetaData.EventId, domainEvent.GetType().FullName, true, SerializeData(domainEvent), SerializeData(domainEvent.MetaData));

            await _connection.AppendToStreamAsync($"{domainEvent.DomainType}-{domainEvent.AggregateRoot}", ExpectedVersion.Any, eventData);
        }

        private static byte[] SerializeData(object data)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
        }
    }
}