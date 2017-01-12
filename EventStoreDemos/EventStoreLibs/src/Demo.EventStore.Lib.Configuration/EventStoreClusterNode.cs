namespace Demo.EventStore.Lib.Configuration
{
    using Common;

    public class EventStoreClusterNode : IEventStoreClusterNode
    {
        public int Number { get; internal set; }

        public string IpAddress { get; internal set; }

        public string HostName { get; internal set; }

        public int ExternalPort { get; internal set; }

        public bool HostNameSpecified => !string.IsNullOrWhiteSpace(HostName);
    }
}