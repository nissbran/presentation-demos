namespace Demo.EventStore.Lib.Subscribers
{
    using System.Collections.Generic;

    public interface IEventStoreClusterConfiguration
    {
        bool UseSsl { get; }

        IEnumerable<IEventStoreClusterNode> ClusterNodes { get; }
    }
}