namespace Demo.EventStore.Lib.Subscribers
{
    using System;
    using System.Collections.Concurrent;
    using global::EventStore.ClientAPI;

    internal abstract class EventStorePersistentNoDuplicateSubscriber : EventStorePersistentSubscriber
    {
        private readonly ISubscriptionLogger _logger;
        private readonly ConcurrentDictionary<Guid, ReceivedEvent> _receivedEvents = new ConcurrentDictionary<Guid, ReceivedEvent>();
        private readonly ConcurrentDictionary<Guid, ProcessedEvent> _processedEvents = new ConcurrentDictionary<Guid, ProcessedEvent>();

        protected EventStorePersistentNoDuplicateSubscriber(IEventStoreConnection connection, ISubscriptionLogger logger, IEventStorePersistentSubscriberSettings settings) 
            : base(connection, logger, settings)
        {
            _logger = logger;
        }

        protected abstract void NewEventAppeared(ResolvedEvent resolvedEvent);

        protected virtual void AlreadyProcessedEventAppeared(ResolvedEvent resolvedEvent)
        {
        }

        protected virtual void OldUnprocessedEventAppeared(ResolvedEvent resolvedEvent)
        {
        }

        protected override void EventAppeared(EventStorePersistentSubscriptionBase subscription, ResolvedEvent resolvedEvent)
        {
            try
            {
                var eventIsNew = _receivedEvents.TryAdd(resolvedEvent.Event.EventId, new ReceivedEvent());

                if (eventIsNew)
                {
                    NewEventAppeared(resolvedEvent);
                }
                else
                {
                    var eventIsProcessed = _processedEvents.ContainsKey(resolvedEvent.Event.EventId);

                    if (eventIsProcessed)
                    {
                        AlreadyProcessedEventAppeared(resolvedEvent);
                    }
                    else
                    {
                        OldUnprocessedEventAppeared(resolvedEvent);
                    }
                }

                subscription.Acknowledge(resolvedEvent);
                
                _processedEvents.TryAdd(resolvedEvent.Event.EventId, new ProcessedEvent());
            }
            catch (AggregateException e)
            {
                _logger.LogError("An error occurred when receiving an event from EventStore, ", e);
                subscription.Fail(resolvedEvent, PersistentSubscriptionNakEventAction.Park, "Retrying failed, parking message.");
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred when receiving an event from EventStore", e);
                subscription.Fail(resolvedEvent, PersistentSubscriptionNakEventAction.Park, "Retrying failed, parking message.");
            }
        }

        private class ReceivedEvent
        {
            public DateTimeOffset Created { get; } = DateTimeOffset.Now;
        }

        private class ProcessedEvent
        {
            public DateTimeOffset Created { get; } = DateTimeOffset.Now;
        }
    }
}