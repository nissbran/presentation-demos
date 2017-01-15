using System;
using Demo.Bank.EventPublisher.Configuration;
using Demo.Bank.ReadStoreSync.Logger;
using Demo.Bank.ReadStoreSync.Subscribers;
using Demo.EventStore.Lib.Common;

namespace Demo.Bank.ReadStoreSync
{
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
