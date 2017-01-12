// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventStoreConfiguration.cs" company="Collector AB">
//   Copyright © Collector AB. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Demo.Bank.EventPublisher.Configuration
{
    using EventStore.Lib.Common;

    public class EventStoreSingleNodeConfiguration : IEventStoreConfiguration
    {
        public bool UseSingleNode { get; }
        public string SingleNodeConnectionUri { get; }
        public IEventStoreClusterConfiguration ClusterConfiguration { get; }

        public EventStoreSingleNodeConfiguration()
        {
            UseSingleNode = true;
            SingleNodeConnectionUri = "tcp://localhost:1113";
            ClusterConfiguration = null;
        }
    }
}