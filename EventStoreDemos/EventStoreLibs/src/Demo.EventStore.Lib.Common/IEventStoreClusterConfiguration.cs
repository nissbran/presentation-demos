namespace Demo.EventStore.Lib.Common
{
    using System.Collections.Generic;

    public interface IEventStoreClusterConfiguration
    {
        bool UseSsl { get; }

        IEnumerable<IEventStoreClusterNode> ClusterNodes { get; }
    }
}