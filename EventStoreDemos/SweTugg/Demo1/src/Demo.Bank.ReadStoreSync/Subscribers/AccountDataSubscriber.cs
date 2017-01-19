namespace Demo.Bank.ReadStoreSync.Subscribers
{
    using System;
    using System.Text;
    using Demo.Bank.ReadStoreSync.Handlers;
    using Demo.EventStore.Lib.Common.Domain;
    using Newtonsoft.Json;

    using System.Threading;
    using EventStore.Lib.Subscribers;
    using global::EventStore.ClientAPI;
    using Logger;

    public class AccountDataSubscriber : EventStorePersistentRegularSubscriber
    {
        public static long Counter = 0;

        private const string AccountStreamPrefix = "account-";

        private readonly AccountBalanceReadModelHandler _accountBalanceReadModelHandler;

        public AccountDataSubscriber(IEventStoreConnection connection, RedisRepository redisRepository)
            : base(connection, new SubscriptionLogger(), new AccountDataSubscriberSettings())
        {
            _accountBalanceReadModelHandler = new AccountBalanceReadModelHandler(redisRepository);
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
            if (!IsValidEvent(resolvedEvent))
                return;

            Interlocked.Increment(ref Counter);

            var domainEvent = ConvertEventDataToDomainEvent(resolvedEvent);
            var accountNumber = GetAccountNumber(resolvedEvent);

            _accountBalanceReadModelHandler.UpdateReadModel(accountNumber, domainEvent);
        }

        private bool IsValidEvent(ResolvedEvent resolvedEvent)
        {
            if (resolvedEvent.Event == null ||
                !resolvedEvent.Event.IsJson ||
                !resolvedEvent.Event.EventStreamId.StartsWith(AccountStreamPrefix))
                return false;

            return true;
        }

        private IDomainEvent ConvertEventDataToDomainEvent(ResolvedEvent resolvedEvent)
        {
            var metadataString = Encoding.UTF8.GetString(resolvedEvent.Event.Metadata);
            var eventString = Encoding.UTF8.GetString(resolvedEvent.Event.Data);

            var metadata = JsonConvert.DeserializeObject<DomainMetadata>(metadataString);
            var eventType = Type.GetType(metadata.EventClrType);
            metadata.EventType = eventType;

            var domainEvent = (IDomainEvent)JsonConvert.DeserializeObject(eventString, eventType);
            domainEvent.Metadata = metadata;

            return domainEvent;
        }

        private string GetAccountNumber(ResolvedEvent resolvedEvent)
        {
            var stream = resolvedEvent.Event.EventStreamId;

            return stream.Substring(AccountStreamPrefix.Length);
        }
    }
}