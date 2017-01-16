namespace Demo.Bank.EventPublisher.Configuration
{
    using System;
    using System.Collections.Generic;
    using EventStore.Lib.Common;

    public class EventStore3NodeClusterConfiguration : IEventStoreConfiguration
    {
        public bool UseSingleNode { get; }
        public string SingleNodeConnectionUri { get; } = string.Empty;
        public IEventStoreClusterConfiguration ClusterConfiguration { get; } = new EventStoreClusterConfiguration();

        public EventStore3NodeClusterConfiguration()
        {
            UseSingleNode = false;
        }
    }

    public class EventStoreClusterConfiguration : IEventStoreClusterConfiguration
    {
        private const string IpAddress = "192.168.99.100";

        public IEnumerable<IEventStoreClusterNode> ClusterNodes { get; } = new[]{
            new ClusterNode(1, IpAddress, 2114),
            new ClusterNode(2, IpAddress, 2124),
            new ClusterNode(3, IpAddress, 2134),
        };

        public bool UseSsl { get; } = false;
    }

    public class ClusterNode : IEventStoreClusterNode
    {
        public int Number { get; }

        public string IpAddress { get; }

        public int ExternalPort { get; }

        public string HostName { get; } = string.Empty;

        public bool HostNameSpecified { get; } = false;

        public ClusterNode(int number, string ipAddress, int externalPort)
        {
            Number = number;
            IpAddress = ipAddress;
            ExternalPort = externalPort;
        }
    }
}