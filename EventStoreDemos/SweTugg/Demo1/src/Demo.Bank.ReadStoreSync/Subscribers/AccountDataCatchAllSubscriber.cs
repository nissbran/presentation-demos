namespace Demo.Bank.ReadStoreSync.Subscribers
{
    using System;
    using System.Text;
    using System.Threading;
    using Configuration;
    using EventStore.Lib.Common.Domain;
    using EventStore.Lib.Subscribers;
    using global::EventStore.ClientAPI;
    using Handlers;
    using Logger;
    using Newtonsoft.Json;
    using Repositories;

    public class AccountDataCatchAllSubscriber : EventStoreCatchAllSubscriber
    {
        public static long Counter = 0;

        private readonly AccountBalanceReadModelHandler _accountBalanceReadModelHandler;
        private readonly AccountInformationHandler _accountInformationHandler;
        private readonly SubscriptionLogger _logger = new SubscriptionLogger();
        private const string AccountStreamPrefix = "account-";

        public AccountDataCatchAllSubscriber(IEventStoreConnection connection, RedisRepository redisRepository, AccountInformationRepository accountInformationRepository) 
            : base(connection, new SubscriptionLogger(), new AccountDataCatchAllSubscriberSettings())
        {
            _accountInformationHandler = new AccountInformationHandler(accountInformationRepository);
            _accountBalanceReadModelHandler = new AccountBalanceReadModelHandler(redisRepository);

            var position = accountInformationRepository.GetStartingPosition();
            SubscriptionPosition = new Position(position, position);
        }

        public void Start()
        {
            SetupConnectionEventListeners();
        }

        protected override void EventAppeared(ResolvedEvent resolvedEvent)
        {
            if (!IsValidEvent(resolvedEvent))
                return;

            Interlocked.Increment(ref Counter);

            var domainEvent = ConvertEventDataToDomainEvent(resolvedEvent);
            var accountNumber = GetAccountNumber(resolvedEvent);

            _accountInformationHandler.UpdateReadModel(
                accountNumber, 
                domainEvent, 
                resolvedEvent.Event.EventNumber, 
                resolvedEvent.OriginalPosition.Value.CommitPosition);
            //_accountBalanceReadModelHandler.UpdateReadModel(accountNumber, domainEvent);
        }

        protected override void LiveProcessingStarted(EventStoreCatchUpSubscription subscription)
        {
            _logger.LogInformation("Live processing started...");
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