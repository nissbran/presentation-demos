namespace Demo.Bank.EventPublisher.Logger
{
    using System;
    using global::EventStore.ClientAPI;

    public class EventStoreLogger : ILogger
    {
        public void Error(string format, params object[] args)
        {
           Console.WriteLine(format, args);
        }

        public void Error(Exception ex, string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void Info(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void Info(Exception ex, string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void Debug(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void Debug(Exception ex, string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }
    }
}
