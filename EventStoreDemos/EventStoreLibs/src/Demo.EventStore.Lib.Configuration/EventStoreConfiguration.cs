namespace Demo.EventStore.Lib.Configuration
{
    using Common;

    public class EventStoreConfiguration : IEventStoreConfiguration
    {
        public bool UseEnvironmentVariableSetting { get; internal set; }

        public bool UseSingleNode { get; internal set; }

        public string SingleNodeConnectionUri { get; internal set; }

        public IEventStoreClusterConfiguration ClusterConfiguration { get; internal set; }
    }
}
