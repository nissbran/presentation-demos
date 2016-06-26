using System;

namespace EventDomain.Model
{
    public class Metadata : IMetaData
    {
        public DateTimeOffset Created { get; set; }
    }
}