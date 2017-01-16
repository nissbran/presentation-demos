namespace Demo.EventStore.Lib.IntegrationTests.Write
{
    using System;
    using System.Collections.Generic;
    using Common.Domain;

    public class TestRoot : IAggregateRoot
    {
        public string Id { get; }
        public int Version { get; }
        public Type AggregateRootType { get; }

        public void LoadFromHistoricalEvents(IEnumerable<IDomainEvent> domainEvents)
        {
            throw new NotImplementedException();
        }
    }
}