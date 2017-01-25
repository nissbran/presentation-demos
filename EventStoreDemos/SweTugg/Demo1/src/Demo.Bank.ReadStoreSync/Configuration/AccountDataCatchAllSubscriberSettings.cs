namespace Demo.Bank.ReadStoreSync.Configuration
{
    using EventStore.Lib.Subscribers;

    public class AccountDataCatchAllSubscriberSettings : IEventStoreCatchAllSubscriberSettings
    {
        public int MaxRetryAttempts { get; } = 3;
        public int StopSubscriptionTimeout { get; } = 5000;
        public int ReadBatchSize { get; } = 500;
    }
}