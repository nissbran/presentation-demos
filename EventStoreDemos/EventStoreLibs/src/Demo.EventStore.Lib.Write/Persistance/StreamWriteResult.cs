namespace Demo.EventStore.Lib.Write.Persistance
{
    public class StreamWriteResult
    {
        public long NextStreamEventNumber { get; }

        public bool EventsWereSaved => NextStreamEventNumber > -1;

        public StreamWriteResult(long nextExpectedVersion)
        {
            NextStreamEventNumber = nextExpectedVersion;
        }
    }
}