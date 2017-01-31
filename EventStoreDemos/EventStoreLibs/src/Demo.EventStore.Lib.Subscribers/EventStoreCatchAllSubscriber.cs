namespace Demo.EventStore.Lib.Subscribers
{
    using System;
    using System.Threading.Tasks;
    using global::EventStore.ClientAPI;
    using global::EventStore.ClientAPI.Exceptions;

    public abstract class EventStoreCatchAllSubscriber : EventStoreSubscriber
    {
        private readonly IEventStoreCatchAllSubscriberSettings _settings;

        private readonly object _subscriptionModificationLock = new object();

        private int _retryAttempts;

        private EventStoreAllCatchUpSubscription _catchUpSubscription;

        protected EventStoreCatchAllSubscriber(
            IEventStoreConnection connection, 
            ISubscriptionLogger logger,
            IEventStoreCatchAllSubscriberSettings settings) : base(connection, logger)
        {
            _settings = settings;

            SubscriptionPosition = new Position(0, 0);
        }

        protected Position SubscriptionPosition { get; set; }
        
        protected void StopSubscriber()
        {
            StopSubscription();

            CloseConnection();
        }

        private void StartSubscription()
        {
            Task.Run(() =>
            {
                lock (_subscriptionModificationLock)
                {
                    //The queue and read batch size is taken from the client apis default settings.
                    var settings = new CatchUpSubscriptionSettings(10000, _settings.ReadBatchSize, false, false);

                    try
                    {
                        _catchUpSubscription?.Stop(new TimeSpan(0, 0, 0, 0, _settings.StopSubscriptionTimeout));
                    }
                    catch (TimeoutException e)
                    {
                        Logger.LogError("Could not stop the subscription...", e);
                    }

                    _catchUpSubscription = Connection.SubscribeToAllFrom(SubscriptionPosition, settings, EventAppeared, LiveProcessingStarted, SubscriptionDropped);
                }
            });
        }

        private void SubscriptionDropped(EventStoreCatchUpSubscription subscription, SubscriptionDropReason reason, Exception exception)
        {
            subscription.Stop();

            if (reason == SubscriptionDropReason.UserInitiated)
                return;

            if (reason == SubscriptionDropReason.EventHandlerException ||
                reason == SubscriptionDropReason.CatchUpError)
            {
                var serverError = exception as ServerErrorException;
                if (serverError != null)
                {
                    OnEventStoreServerError(serverError);
                    return;
                }

                StartSubscription();
                return;
            }

            OnEventStoreSubscriptionDropped(subscription, reason, exception);
        }

        public void StopSubscription()
        {
            lock (_subscriptionModificationLock)
            {
                Logger.LogInformation("Stopping catch all subscription...");
                try
                {
                    _catchUpSubscription?.Stop(new TimeSpan(0, 0, 0, 0, _settings.StopSubscriptionTimeout));
                }
                catch (TimeoutException e)
                {
                    Logger.LogError("Could not stop the subscription...", e);
                }
            }
        }

        private void EventAppeared(EventStoreCatchUpSubscription subscription, ResolvedEvent resolvedEvent)
        {
            if (resolvedEvent.OriginalPosition == null)
            {
                return;
            }
            
            var currentPosition = resolvedEvent.OriginalPosition.Value;

            try
            {
                EventAppeared(resolvedEvent);
            }
            catch (Exception e)
            {
                if (Retry())
                {
                    Logger.LogWarning("Exception recieved. Retrying...");
                    throw;
                }
                ExceptionRecievedAfterMaxRetries(e);
            }

            SubscriptionPosition = currentPosition;
        }

        private bool Retry()
        {
            _retryAttempts++;

            if (_retryAttempts >= _settings.MaxRetryAttempts)
            {
                _retryAttempts = 0;
                return false;
            }
            return true;
        }

        protected abstract void EventAppeared(ResolvedEvent resolvedEvent);

        protected abstract void LiveProcessingStarted(EventStoreCatchUpSubscription subscription);

        protected virtual void ExceptionRecievedAfterMaxRetries(Exception exception)
        {
            Logger.LogError($"An error occurred when receiving an event from EventStore. Retried {_settings.MaxRetryAttempts} times.", exception);
        }

        protected virtual void OnEventStoreSubscriptionDropped(EventStoreCatchUpSubscription subscription, SubscriptionDropReason reason, Exception exception)
        {
        }

        internal override void OnEventStoreConnected(object sender, ClientConnectionEventArgs clientConnectionEventArg)
        {
            StartSubscription();
        }

        internal override void OnEventStoreDisconnected(object sender, ClientConnectionEventArgs clientConnectionEventArgs)
        {
            lock (_subscriptionModificationLock)
            {
                _catchUpSubscription.Stop();
            }
        }

        internal override void OnEventStoreConnectionClosed(object sender, ClientClosedEventArgs clientClosedEventArgs)
        {
            lock (_subscriptionModificationLock)
            {
                _catchUpSubscription.Stop();
            }
        }
    }
}
