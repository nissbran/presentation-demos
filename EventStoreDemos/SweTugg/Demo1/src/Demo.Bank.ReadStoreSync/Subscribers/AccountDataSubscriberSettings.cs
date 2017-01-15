using Demo.EventStore.Lib.Subscribers;

namespace Demo.Bank.ReadStoreSync.Subscribers
{
    public class AccountDataSubscriberSettings : IEventStorePersistentSubscriberSettings
    {
        public int StopSubscriptionTimeout { get; } = 3000;
        public string Stream { get; } = "Account-Test";
        public string SubscriptionGroup { get; } = "TestGroup";
        public int NumberOfParrallelSubscribers { get; } = 10;
    }
}