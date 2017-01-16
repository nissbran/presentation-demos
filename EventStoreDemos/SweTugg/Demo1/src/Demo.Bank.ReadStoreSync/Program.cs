namespace Demo.Bank.ReadStoreSync
{
    using System;
    using Configuration;
    using EventStore.Lib.Common;
    using Logger;
    using Subscribers;

    public class Program
    {
        public static void Main(string[] args)
        {
            var eventStoreConnection = EventStoreConnectionFactory.Create(
                   new EventStoreSingleNodeConfiguration(),
                   new EventStoreLogger(),
                   "admin", "changeit");

            var accountDataSubscriber = new AccountDataSubscriber(eventStoreConnection);

            accountDataSubscriber.Start();

            Console.ReadLine();

            Console.WriteLine($"Number of events: {AccountDataSubscriber.Counter}");

            accountDataSubscriber.Stop();

            Console.ReadLine();
        }
    }
}
