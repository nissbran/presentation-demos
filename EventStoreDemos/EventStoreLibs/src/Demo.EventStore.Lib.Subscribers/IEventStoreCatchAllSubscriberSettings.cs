namespace Demo.EventStore.Lib.Subscribers
{
    public interface IEventStoreCatchAllSubscriberSettings
    {
        int MaxRetryAttempts { get; }

        int StopSubscriptionTimeout { get; }

        int ReadBatchSize { get; }
    }
}