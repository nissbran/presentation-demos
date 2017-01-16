namespace Demo.EventStore.Lib.Common.Domain
{
    using System;
    using System.Collections.Generic;

    public interface IAggregateRoot
    {
        string Id { get; set; }

        int Version { get; set; }

        Type AggregateRootType { get; }

        void LoadFromHistoricalEvents(IEnumerable<IDomainEvent> domainEvents);

        List<IDomainEvent> UncommittedEvents { get; }

        void CommitEvents();
    }
}