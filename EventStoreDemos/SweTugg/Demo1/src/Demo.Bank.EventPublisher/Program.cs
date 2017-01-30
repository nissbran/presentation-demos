namespace Demo.Bank.EventPublisher
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Models.Account;
    using EventStore.Lib.Common;
    using EventStore.Lib.Common.Configurations;
    using EventStore.Lib.Write.Persistance;
    using Logger;

    public class Program
    {
        private static long NumberOfEventsPublished = 0;

        public static void Main(string[] args)
        {
            var eventStoreConnection = EventStoreConnectionFactory.Create(
                new EventStore3NodeClusterConfiguration(), 
                new EventStoreLogger(),
                "admin", "changeit");

            const string accountNumberPrefix = "1206";

            eventStoreConnection.ConnectAsync().Wait();

            var eventStore = new EventStoreDomainRepository(eventStoreConnection);

            Task.Run(async () => {
                while (true)
                {
                    await Task.Delay(2000);
                    Console.WriteLine($"Number of events published: {Interlocked.Read(ref NumberOfEventsPublished)}");
                }
            });

            var tasks = new Task[50];

            for (int i = 0; i < 50; i++)
            {
                try
                {
                    var account = eventStore.GetById<Account>($"{accountNumberPrefix}-{i}").Result;
                }
                catch (Exception)
                {
                    var account = new Account($"{accountNumberPrefix}-{i}");
                    eventStore.Save(account).Wait();
                    Interlocked.Increment(ref NumberOfEventsPublished);
                }

            }
            for (int i = 0; i < 50; i++)
            {
                var accountNumber = i;

                tasks[i] = Task.Run(async () =>
                {
                    for (int j = 0; j < 50; j++)
                    {
                        var account = await eventStore.GetById<Account>($"{accountNumberPrefix}-{accountNumber}");
                        //await Task.Delay(500);
                        account.AddBankTransferTransaction(5);
                        account.AddBankTransferTransaction(5);
                        account.AddBankTransferTransaction(5);
                        account.AddBankTransferTransaction(5);

                        Interlocked.Add(ref NumberOfEventsPublished, 4);

                        await eventStore.Save(account);
                    }
                });
            }

            Task.WaitAll(tasks);
            
            Console.WriteLine("All done");

            Console.ReadLine();
        }
    }
}
