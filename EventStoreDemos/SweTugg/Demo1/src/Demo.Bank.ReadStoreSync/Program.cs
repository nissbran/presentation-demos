namespace Demo.Bank.ReadStoreSync
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventStore.Lib.Common;
    using EventStore.Lib.Common.Configurations;
    using Logger;
    using Subscribers;
    using Domain.ReadModels;

    public class Program
    {
        public static void Main(string[] args)
        {
            var eventStoreConnection = EventStoreConnectionFactory.Create(
                   new EventStoreSingleNodeConfiguration(), 
                   new EventStoreLogger(),
                   "admin", "changeit");

            var redisRepository = new RedisRepository();

            var accountDataSubscriber = new AccountDataSubscriber(eventStoreConnection, redisRepository);

            accountDataSubscriber.Start();

            var accountCatchAllSubscriber = new AccountDataCatchAllSubscriber(eventStoreConnection, redisRepository);

            accountCatchAllSubscriber.Start();

            eventStoreConnection.ConnectAsync().Wait();
            
            var monitorCancellationTokenSource = new CancellationTokenSource();
            var token = monitorCancellationTokenSource.Token;
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(1000, token);
                    Console.WriteLine($"Number of persistent subscription events processed: {Interlocked.Read(ref AccountDataSubscriber.Counter)}");
                    Console.WriteLine($"Number of catchall subscription events processed: {Interlocked.Read(ref AccountDataCatchAllSubscriber.Counter)}");
                }
            }, token);

            Console.ReadLine();
            
            accountDataSubscriber.Stop();

            accountCatchAllSubscriber.StopSubscription();

            eventStoreConnection.Close();

            monitorCancellationTokenSource.Cancel();

            for (int i = 0; i < 50; i++)
            {
                var data = redisRepository.Get<AccountBalanceReadModel>($"balance-1206-{i}");

                if (data.Balance != 4000)
                {
                    Console.WriteLine($"Error {data.AccountNumber}, balance: {data.Balance}");
                }
            }

            Console.ReadLine();
        }
    }
}
