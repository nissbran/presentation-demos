namespace Demo.Bank.EventPublisher
{
    using System;
    using Configuration;
    using EventStore.Lib.Common;
    using Logger;

    public class Program
    {
        public static void Main(string[] args)
        {
            var eventStoreConnection = EventStoreConnectionFactory.Create(
                new EventStoreSingleNodeConfiguration(),
                new EventStoreLogger(), 
                "admin", "changeit");

            eventStoreConnection.ConnectAsync().Wait();



            Console.ReadLine();
        }
    }
}
