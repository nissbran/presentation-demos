using System.Threading;
using Demo.Bank.ReadStoreSync.Logger;
using Demo.EventStore.Lib.Subscribers;
using EventStore.ClientAPI;

namespace Demo.Bank.ReadStoreSync.Subscribers
{
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