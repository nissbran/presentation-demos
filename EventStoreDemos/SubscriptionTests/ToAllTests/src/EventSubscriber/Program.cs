using System;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;

namespace EventSubscriber
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connection = EventStoreConnection.Create(new Uri("tcp://localhost:1113"));

            connection.ConnectAsync().Wait();


            try
            {
                connection.CreatePersistentSubscriptionAsync(
                    "Transactions",
                    "TestGroup",
                    PersistentSubscriptionSettings
                        .Create()
                        .CheckPointAfter(TimeSpan.FromSeconds(5))
                        .Build(), new UserCredentials("admin", "changeit")).Wait();
            }
            catch (AggregateException e)
            {
                Console.WriteLine($"Subscription already exist.");
            }

            //connection.ConnectToPersistentSubscriptionAsync("Transactions", "TestGroup", EventAppeared);

            Console.ReadLine();
        }

        private static void EventAppeared(EventStorePersistentSubscriptionBase eventStorePersistentSubscriptionBase, ResolvedEvent resolvedEvent)
        {
            Console.WriteLine($"Event recieved: {resolvedEvent.Event.EventId}");
        }
    }
}
