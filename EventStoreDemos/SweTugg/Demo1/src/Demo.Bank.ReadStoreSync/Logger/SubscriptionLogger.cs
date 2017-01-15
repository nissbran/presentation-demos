using System;
using Demo.EventStore.Lib.Subscribers;

namespace Demo.Bank.ReadStoreSync.Logger
{
    public class SubscriptionLogger : ISubscriptionLogger
    {
        public void LogDebug(string message, Exception exception = null)
        {
            Console.WriteLine(message);
        }

        public void LogInformation(string message, Exception exception = null)
        {
            Console.WriteLine(message);
        }

        public void LogWarning(string message, Exception exception = null)
        {
            Console.WriteLine(message);
        }

        public void LogError(string message, Exception exception = null)
        {
            Console.WriteLine(message);
        }

        public void LogCritical(string message, Exception exception = null)
        {
            Console.WriteLine(message);
        }
    }
}