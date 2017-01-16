namespace Demo.Bank.ReadStoreSync.Subscribers
{
    using System.Threading;
    using EventStore.Lib.Subscribers;
    using global::EventStore.ClientAPI;
    using Logger;

    public class AccountDataSubscriber : EventStorePersistentRegularSubscriber
    {
        public static long Counter = 0;

        public AccountDataSubscriber(IEventStoreConnection connection)
            : base(connection, new SubscriptionLogger(), new AccountDataSubscriberSettings())
        {

        }

        public void Start()
        {
            ConnectToEventStore();
        }

        public void Stop()
        {
            StopSubscriber();
        }

        protected override void EventAppeared(ResolvedEvent resolvedEvent)
        {
            Interlocked.Increment(ref Counter);
        }
    }
}