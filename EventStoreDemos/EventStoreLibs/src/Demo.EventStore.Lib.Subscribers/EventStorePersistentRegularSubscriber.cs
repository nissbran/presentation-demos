namespace Demo.EventStore.Lib.Subscribers
{
    using System;
    using global::EventStore.ClientAPI;

    public abstract class EventStorePersistentRegularSubscriber : EventStorePersistentSubscriber
    {
        private readonly ISubscriptionLogger _logger;

        protected EventStorePersistentRegularSubscriber(IEventStoreConnection connection, ISubscriptionLogger logger,
            IEventStorePersistentSubscriberSettings settings) : base(connection, logger, settings)
        {
            _logger = logger;
        }

        protected abstract void EventAppeared(ResolvedEvent resolvedEvent);

        protected override void EventAppeared(EventStorePersistentSubscriptionBase subscription, ResolvedEvent resolvedEvent)
        {
            try
            {
                EventAppeared(resolvedEvent);
                subscription.Acknowledge(resolvedEvent);
            }
            catch (AggregateException e)
            {
                _logger.LogCritical("An error occurred when receiving an event from EventStore, message parked.", e);
                subscription.Fail(resolvedEvent, PersistentSubscriptionNakEventAction.Park, "Exception in event appeared failed, parking message.");
            }
            catch (Exception e)
            {
                _logger.LogCritical("An error occurred when receiving an event from EventStore, message parked.", e);
                subscription.Fail(resolvedEvent, PersistentSubscriptionNakEventAction.Park, "Exception in event appeared failed, parking message.");
            }
        }
    }
}