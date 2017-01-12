namespace Demo.EventStore.Lib.Subscribers
{
    public interface IEventStoreConfiguration
    {
        bool UseSingleNode { get; }

        string SingleNodeConnectionUri { get; }

        IEventStoreClusterConfiguration ClusterConfiguration { get; }
    }
}