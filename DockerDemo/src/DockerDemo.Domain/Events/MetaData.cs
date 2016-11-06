namespace DockerDemo.Domain
{
    using System;

    public class DomainMetaData
    {
        public Guid EventId {get;set;}
        public DateTimeOffset Created { get; set; }
    }
}