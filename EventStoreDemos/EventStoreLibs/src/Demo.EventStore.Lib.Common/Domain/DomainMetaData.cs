namespace Demo.EventStore.Lib.Common.Domain
{
    using System;
    using Newtonsoft.Json;

    public class DomainMetadata
    {
        public Guid CommitId { get; set; }

        public string EventClrType { get; set; }

        [JsonIgnore]
        public Type EventType { get; set; }
    }
}