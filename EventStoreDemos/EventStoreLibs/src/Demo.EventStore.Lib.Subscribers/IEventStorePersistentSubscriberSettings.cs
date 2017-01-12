namespace Demo.EventStore.Lib.Subscribers
{
    public interface IEventStorePersistentSubscriberSettings
    {
        int StopSubscriptionTimeout { get; }

        string Stream { get; }
        
        string SubscriptionGroup { get; }

        int NumberOfParrallelSubscribers { get; }
    }
}