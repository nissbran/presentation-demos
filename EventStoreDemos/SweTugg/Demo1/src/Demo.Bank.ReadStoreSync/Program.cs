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
    using Repositories;

    public class Program
    {
        private const bool UseCatchAll = true;
        private const bool UseCache = true;

        private static AccountDataSubscriber _accountDataSubscriber;
        private static AccountDataCatchAllSubscriber _accountDataCatchAllSubscriber;

        public static void Main(string[] args)
        {
            var eventStoreConnection = EventStoreConnectionFactory.Create(
                   new EventStore3NodeClusterConfiguration(), 
                   new EventStoreLogger(),
                   "admin", "changeit");

            var redisRepository = new RedisRepository();
            var accountInformationRepository = new AccountInformationRepository();

            if (UseCache)
            {
                _accountDataSubscriber = new AccountDataSubscriber(eventStoreConnection, redisRepository);

                _accountDataSubscriber.Start();
            }

            if (UseCatchAll)
            {
                _accountDataCatchAllSubscriber = new AccountDataCatchAllSubscriber(eventStoreConnection, redisRepository, accountInformationRepository);

                _accountDataCatchAllSubscriber.Start();
            }

            eventStoreConnection.ConnectAsync().Wait();
            
            var monitorCancellationTokenSource = new CancellationTokenSource();
            var token = monitorCancellationTokenSource.Token;
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(1000, token);
                    if (UseCache)
                        Console.WriteLine($"Number of persistent subscription events processed: {Interlocked.Read(ref AccountDataSubscriber.Counter)}");
                    
                    if (UseCatchAll)
                        Console.WriteLine($"Number of catchall subscription events processed: {Interlocked.Read(ref AccountDataCatchAllSubscriber.Counter)}");
                }
            }, token);

            Console.ReadLine();
            if (UseCache)
            {
                _accountDataSubscriber.Stop();
            }
            if (UseCatchAll)
            {
                _accountDataCatchAllSubscriber.StopSubscription();
            }

            eventStoreConnection.Close();

            monitorCancellationTokenSource.Cancel();

            // for (int i = 0; i < 50; i++)
            // {
            //     var data = redisRepository.Get<AccountBalanceReadModel>($"balance-1206-{i}");

            //     if (data.Balance != 2000)
            //     {
            //         Console.WriteLine($"Error {data.AccountNumber}, balance: {data.Balance}");
            //     }
            // }

            Console.ReadLine();
        }
    }
}
