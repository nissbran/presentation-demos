namespace Demo.Bank.ReadStoreSync.Subscribers
{
    using EventStore.Lib.Subscribers;

    public class AccountDataSubscriberSettings : IEventStorePersistentSubscriberSettings
    {
        public int StopSubscriptionTimeout { get; } = 3000;
        public string Stream { get; } = "$ce-account";
        public string SubscriptionGroup { get; } = "AccountReadStoreSync";
        public int NumberOfParrallelSubscribers { get; } = 10;
    }
}