namespace Demo.EventStore.Lib.Configuration
{
    using System.Collections.Generic;

    public interface IEventStoreClusterConfiguration
    {
        bool UseSsl { get; }

        IEnumerable<IEventStoreClusterNode> ClusterNodes { get; }
    }
}