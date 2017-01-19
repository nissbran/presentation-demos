using Demo.Bank.Domain.ReadModels;

namespace Demo.Bank.ReadStoreSync
{
    using System;
    using EventStore.Lib.Common;
    using EventStore.Lib.Common.Configurations;
    using Logger;
    using Subscribers;

    public class Program
    {
        public static void Main(string[] args)
        {
            var eventStoreConnection = EventStoreConnectionFactory.Create(
                   new EventStore3NodeClusterConfiguration(), 
                   new EventStoreLogger(),
                   "admin", "changeit");

            var redisRepository = new RedisRepository();

            var accountDataSubscriber = new AccountDataSubscriber(eventStoreConnection, redisRepository);

            accountDataSubscriber.Start();

            Console.ReadLine();

            Console.WriteLine($"Number of events processed: {AccountDataSubscriber.Counter}");

            accountDataSubscriber.Stop();

            for (int i = 0; i < 100; i++)
            {
                var data = redisRepository.Get<AccountBalanceReadModel>($"balance-test6-{i}");

                if (data.Balance != 1000)
                {
                    Console.WriteLine($"Error {data.AccountNumber}, balance: {data.Balance}");
                }
            }

            Console.ReadLine();
        }
    }
}
