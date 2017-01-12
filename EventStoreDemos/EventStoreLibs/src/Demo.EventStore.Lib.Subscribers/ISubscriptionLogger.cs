namespace Demo.EventStore.Lib.Subscribers
{
    using System;

    public interface ISubscriptionLogger
    {
        void LogDebug(string message, Exception exception = null);
        
        void LogInformation(string message, Exception exception = null);
        
        void LogWarning(string message, Exception exception = null);
        
        void LogError(string message, Exception exception = null);
        
        void LogCritical(string message, Exception exception = null);
    }
}