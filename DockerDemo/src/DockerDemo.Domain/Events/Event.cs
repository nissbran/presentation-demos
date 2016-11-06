namespace DockerDemo.Domain
{
    using Newtonsoft.Json;

    public abstract class Event
    {
        public string AggregateRoot { get; set; }

        [JsonIgnore]
        public abstract string DomainType { get; }

        [JsonIgnore]
        public DomainMetaData MetaData { get; set; }
    }
}