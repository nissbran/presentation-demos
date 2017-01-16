namespace Demo.EventStore.Lib.IntegrationTests.Write
{
    using System;
    using System.Collections.Generic;
    using Common.Domain;

    public class TestRoot : IAggregateRoot
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public Type AggregateRootType { get; }

        public void LoadFromHistoricalEvents(IEnumerable<IDomainEvent> domainEvents)
        {
            throw new NotImplementedException();
        }

        public List<IDomainEvent> UncommittedEvents { get; }
        public void CommitEvents()
        {
            throw new NotImplementedException();
        }
    }
}