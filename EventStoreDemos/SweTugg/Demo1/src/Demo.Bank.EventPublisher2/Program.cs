namespace Demo.Bank.EventPublisher2
{
    using System;
    using System.Threading.Tasks;
    using Domain.Models.Account;
    using EventPublisher.Logger;
    using EventStore.Lib.Common;
    using EventStore.Lib.Common.Configurations;
    using EventStore.Lib.Write.Persistance;

    public class Program
    {
        public static void Main(string[] args)
        {
            var eventStoreConnection = EventStoreConnectionFactory.Create(
                 new EventStore3NodeClusterConfiguration(),
                 new EventStoreLogger(),
                 "admin", "changeit");

            const string accountNumberPrefix = "1206";

            eventStoreConnection.ConnectAsync().Wait();

            var eventStore = new EventStoreDomainRepository(eventStoreConnection);

            var tasks = new Task[50];
            
            for (int i = 0; i < 50; i++)
            {
                var accountNumber = i;

                tasks[i] = Task.Run(async () =>
                {
                    for (int j = 0; j < 50; j++)
                    {
                        var account = await eventStore.GetById<Account>($"{accountNumberPrefix}-{accountNumber}");
                        for (int k = 0; k < 5; k++)
                        {
                            await Task.Delay(1);
                            account.AddCardTransaction(new Random().Next(0, 55000), new Random().Next(10000,99999).ToString());
                        } 
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
