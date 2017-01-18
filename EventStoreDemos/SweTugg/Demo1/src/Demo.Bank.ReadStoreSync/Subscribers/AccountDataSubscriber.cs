namespace Demo.Bank.ReadStoreSync.Subscribers
{
    using System.Threading;
    using EventStore.Lib.Subscribers;
    using StackExchange.Redis;
    using global::EventStore.ClientAPI;
    using Logger;

    public class AccountDataSubscriber : EventStorePersistentRegularSubscriber
    {
        public static long Counter = 0;

        private readonly ConnectionMultiplexer _redisConnection;
        private readonly IDatabase _redisDatabase;

        public AccountDataSubscriber(IEventStoreConnection connection)
            : base(connection, new SubscriptionLogger(), new AccountDataSubscriberSettings())
        {
            _redisConnection = ConnectionMultiplexer.Connect("localhost");
            _redisDatabase = _redisConnection.GetDatabase();
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
            if (resolvedEvent.Event == null)
                return;

            Interlocked.Increment(ref Counter);

            var stream = resolvedEvent.Event.EventStreamId;
            

            var value = _redisDatabase.StringSet(stream, "hej");
            
        }
    }
}