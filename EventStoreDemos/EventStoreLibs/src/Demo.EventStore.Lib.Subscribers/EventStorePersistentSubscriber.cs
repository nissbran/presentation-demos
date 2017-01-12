namespace Demo.EventStore.Lib.Subscribers
{
    using System;
    using System.Threading.Tasks;
    using global::EventStore.ClientAPI;
    using global::EventStore.ClientAPI.Exceptions;

    public abstract class EventStorePersistentSubscriber : EventStoreSubscriber
    {
        private readonly IEventStorePersistentSubscriberSettings _settings;

        protected readonly EventStorePersistentSubscriptionBase[] Subscriptions;

        private readonly object _subscriptionModificationLock = new object();

        protected EventStorePersistentSubscriber(IEventStoreConnection connection, ISubscriptionLogger logger,
            IEventStorePersistentSubscriberSettings settings) : base(connection, logger)
        {
            _settings = settings;
            Subscriptions = new EventStorePersistentSubscriptionBase[settings.NumberOfParrallelSubscribers];
        }
        
        protected void StopSubscriber()
        {
            StopSubscriptions();

            CloseConnection();
        }

        protected void StopSubscriptions()
        {
            lock (_subscriptionModificationLock)
            {
                Logger.LogInformation("Stopping all subscriptions...");

                for (int i = 0; i < Subscriptions.Length; i++)
                {
                    try
                    {
                        var subscription = Subscriptions[i];
                        if (subscription == null)
                            continue;

                        subscription.Stop(new TimeSpan(0, 0, 0, 0, _settings.StopSubscriptionTimeout));
                        Logger.LogInformation($"Subscription {i} have been stopped.");

                    }
                    catch (TimeoutException e)
                    {
                        Logger.LogError($"Could not stop the subscription {i}...", e);
                    }
                }
            }
        }

        private void StartSubscriptions()
        {
            Task.Run(() =>
            {
                lock (_subscriptionModificationLock)
                {
                    Logger.LogInformation($"Starting {_settings.NumberOfParrallelSubscribers} subscriptions...");

                    for (int i = 0; i < Subscriptions.Length; i++)
                    {
                        try
                        {
                            var subscription = Subscriptions[i];
                            if (subscription == null)
                                continue;

                            subscription.Stop(new TimeSpan(0, 0, 0, 0, _settings.StopSubscriptionTimeout));
                            Logger.LogInformation($"Subscription {i} have been stopped.");

                        }
                        catch (TimeoutException e)
                        {
                            Logger.LogError($"Could not stop the subscription {i}.... ", e);
                        }
                    }

                    for (int i = 0; i < Subscriptions.Length; i++)
                    {
                        try
                        {
                            Subscriptions[i] = Connection.ConnectToPersistentSubscriptionAsync(
                                _settings.Stream, _settings.SubscriptionGroup, EventAppeared, SubscriptionDropped, null, 1, false).Result;
                        }
                        catch (AggregateException e)
                        {
                            Logger.LogError($"Could not start the subscription {i}. Reason: {e.InnerException?.Message}", e.InnerException);
                            return;
                        }
                    }

                    Logger.LogInformation("All subscriptions have been started.");
                }
            });
        }

        protected abstract void EventAppeared(EventStorePersistentSubscriptionBase subscription, ResolvedEvent resolvedEvent);
        
        private void SubscriptionDropped(EventStorePersistentSubscriptionBase subscription, SubscriptionDropReason reason, Exception exception)
        {
            try
            {
                OnEventStoreSubscriptionDropped(subscription, reason, exception);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }

            if (reason == SubscriptionDropReason.UserInitiated)
                return;

            if (reason == SubscriptionDropReason.PersistentSubscriptionDeleted)
            {
                Logger.LogError($"Subscription for stream '{_settings.Stream}' and group '{_settings.SubscriptionGroup}' was deleted!!");
                return;
            }

            if (reason == SubscriptionDropReason.EventHandlerException ||
                reason == SubscriptionDropReason.CatchUpError)
            {
                var serverError = exception as ServerErrorException;
                if (serverError != null)
                {
                    OnEventStoreServerError(serverError);
                    return;
                }

                StartSubscriptions();
            }
        }

        internal override void OnEventStoreConnected(object sender, ClientConnectionEventArgs clientConnectionEventArg)
        {
            StartSubscriptions();
        }

        protected virtual void OnEventStoreSubscriptionDropped(EventStorePersistentSubscriptionBase subscription, SubscriptionDropReason reason, Exception exception)
        {
        }
    }
}