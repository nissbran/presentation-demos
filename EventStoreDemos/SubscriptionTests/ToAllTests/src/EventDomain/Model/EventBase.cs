using System;

namespace EventDomain.Model
{
    public abstract class EventBase<TData, TMeta>
        where TData : class
        where TMeta : class, IMetaData
    {
        protected EventBase()
        {
            EventId = Guid.NewGuid();
        }

        public Guid EventId { get; set; }

        public TData Data { get; set; }

        public TMeta MetaData { get; set; }
    }
}