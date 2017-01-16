namespace Demo.EventStore.Lib.Common.Domain
{
    using Newtonsoft.Json;

    public interface IDomainEvent
    {
        [JsonIgnore]
        DomainMetadata Metadata { get; set; }
    }
}