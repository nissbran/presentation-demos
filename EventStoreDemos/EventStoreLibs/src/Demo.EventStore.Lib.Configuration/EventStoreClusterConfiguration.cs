namespace Demo.EventStore.Lib.Configuration
{
    using System.Collections.Generic;
    using Common;

    public class EventStoreClusterConfiguration : IEventStoreClusterConfiguration
    {
        public bool UseSsl { get; internal set; }

        public IEnumerable<IEventStoreClusterNode> ClusterNodes { get; internal set; }
    }
}