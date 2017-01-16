namespace Demo.Bank.Domain
{
    using EventStore.Lib.Common.Domain;

    public class DomainEvent : IDomainEvent
    {
        public DomainMetadata Metadata { get; set; } = new DomainMetadata();
    }
}