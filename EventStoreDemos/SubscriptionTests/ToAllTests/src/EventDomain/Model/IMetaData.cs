using System;

namespace EventDomain.Model
{
    public interface IMetaData
    {
        DateTimeOffset Created { get; set; }
    }
}