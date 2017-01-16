namespace Demo.EventStore.Lib.Write.Persistance
{
    using System;

    public class StreamId
    {
        public string Context { get; }

        public string Id { get; }

        private StreamId(string context, string id)
        {
            Context = context;
            Id = id;
        }

        public StreamId(Type streamType, string id) : this(streamType.Name.ToLowerInvariant(), id)
        {
        }

        public override string ToString()
        {
            return $"{Context}-{Id}";
        }
    }
}