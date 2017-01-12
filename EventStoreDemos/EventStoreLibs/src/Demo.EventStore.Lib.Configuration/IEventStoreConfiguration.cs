namespace Demo.EventStore.Lib.Configuration
{
    public interface IEventStoreConfiguration
    {
        bool UseSingleNode { get; }

        string SingleNodeConnectionUri { get; }

        IEventStoreClusterConfiguration ClusterConfiguration { get; }
    }
}