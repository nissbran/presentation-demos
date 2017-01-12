namespace Demo.EventStore.Lib.Subscribers
{
    using global::EventStore.ClientAPI;
    using global::EventStore.ClientAPI.Exceptions;

    public abstract class EventStoreSubscriber
    {
        protected ISubscriptionLogger Logger { get; }

        protected IEventStoreConnection Connection { get; }

        protected EventStoreSubscriber(IEventStoreConnection connection, ISubscriptionLogger logger)
        {
            Logger = logger;
            Connection = connection;
        }
        
        protected void ConnectToEventStore()
        {
            SetupConnectionEventListeners();

            Connection.ConnectAsync().Wait();
        }

        protected void CloseConnection()
        {
            Connection.Close();
        }

        protected void SetupConnectionEventListeners()
        {
            Connection.AuthenticationFailed += OnAuthenticationFailed;
            Connection.Connected += OnConnected;
            Connection.Disconnected += OnDisconnected;
            Connection.Reconnecting += OnReconnecting;
            Connection.ErrorOccurred += OnErrorOccurred;
            Connection.Closed += OnConnectionClosed;
        }

        private void OnAuthenticationFailed(object sender, ClientAuthenticationFailedEventArgs clientAuthenticationFailedEventArgs)
        {
            OnEventStoreAuthenticationFailed(sender, clientAuthenticationFailedEventArgs);
        }

        private void OnConnected(object sender, ClientConnectionEventArgs clientConnectionEventArgs)
        {
            Logger.LogInformation("Connected, starting or restarting subscriptions.");
            OnEventStoreConnected(sender, clientConnectionEventArgs);
        }

        private void OnDisconnected(object sender, ClientConnectionEventArgs clientConnectionEventArgs)
        {
            Logger.LogInformation("Disconnected, stopping subscriptions.");
            OnEventStoreDisconnected(sender, clientConnectionEventArgs);
        }

        private void OnReconnecting(object sender, ClientReconnectingEventArgs clientReconnectingEventArgs)
        {
            Logger.LogInformation("Reconnecting...");
            OnEventStoreReconnecting(sender, clientReconnectingEventArgs);
        }

        private void OnErrorOccurred(object sender, ClientErrorEventArgs clientErrorEventArgs)
        {
            OnEventStoreErrorOccurred(sender, clientErrorEventArgs);
        }

        private void OnConnectionClosed(object sender, ClientClosedEventArgs clientClosedEventArgs)
        {
            Logger.LogInformation($"Connection Closed: '{clientClosedEventArgs.Reason}'");
            OnEventStoreConnectionClosed(sender, clientClosedEventArgs);
        }

        internal virtual void OnEventStoreAuthenticationFailed(object sender, ClientAuthenticationFailedEventArgs clientAuthenticationFailedEventArgs)
        {
        }

        internal virtual void OnEventStoreConnected(object sender, ClientConnectionEventArgs clientConnectionEventArg)
        {
        }

        internal virtual void OnEventStoreDisconnected(object sender, ClientConnectionEventArgs clientConnectionEventArgs)
        {
        }

        internal virtual void OnEventStoreReconnecting(object sender, ClientReconnectingEventArgs clientReconnectingEventArgs)
        {
        }

        internal virtual void OnEventStoreErrorOccurred(object sender, ClientErrorEventArgs clientErrorEventArgs)
        {
        }

        internal virtual void OnEventStoreConnectionClosed(object sender, ClientClosedEventArgs clientClosedEventArgs)
        {
        }

        protected virtual void OnEventStoreServerError(ServerErrorException serverErrorException)
        {
        }
    }
}