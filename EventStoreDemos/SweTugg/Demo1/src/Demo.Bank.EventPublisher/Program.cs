namespace Demo.Bank.EventPublisher
{
    using System;
    using System.Threading.Tasks;
    using Domain.Models.Account;
    using EventStore.Lib.Common;
    using EventStore.Lib.Common.Configurations;
    using EventStore.Lib.Write.Persistance;
    using Logger;

    public class Program
    {
        public static void Main(string[] args)
        {
            var eventStoreConnection = EventStoreConnectionFactory.Create(
                new EventStoreSingleNodeConfiguration(), 
                new EventStoreLogger(),
                "admin", "changeit");

            const string accountNumberPrefix = "1206";

            eventStoreConnection.ConnectAsync().Wait();

            var eventStore = new EventStoreDomainRepository(eventStoreConnection);

            var tasks = new Task[50];

            for (int i = 0; i < 50; i++)
            {
                try
                {
                    var account = eventStore.GetById<Account>($"{accountNumberPrefix}-{i}").Result;
                }
                catch (Exception e)
                {
                    var account = new Account($"{accountNumberPrefix}-{i}");
                    eventStore.Save(account).Wait();
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
                        await eventStore.Save(account);
                    }
                });
            }

            Task.WaitAll(tasks);

            //var transactionAdded = new BankTransferTransactionAddedEvent
            //{
            //    Amount = new Random().Next(10, 1000),
            //    Vat = 25
            //};

            //var data2 = eventStoreConnection.ReadStreamEventsForwardAsync("Account-Test", 0, 2000, false).Result;

            //var tasks = new Task[100];

            //for (int i = 0; i < 100; i++)
            //{
            //    tasks[i] = Task.Run(async () =>
            //    {
            //        for (int j = 0; j < 500; j++)
            //        {
            //            BankTransferTransactionAddedEvent transactionAdded = new BankTransferTransactionAddedEvent
            //            {
            //                Amount = new Random().Next(10, 1000),
            //                Vat = 25
            //            };

            //            if (j == 300)
            //            {
            //                transactionAdded = new BankTransferTransactionAddedEvent
            //                {
            //                    Amount = 5000,
            //                    Vat = 25
            //                };
            //            }

            //            await eventStoreConnection.AppendToStreamAsync("Account-Test", ExpectedVersion.Any, new EventData(
            //  Guid.NewGuid(),
            //  typeof(BankTransferTransactionAddedEvent).FullName,
            //  true,
            //  Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(transactionAdded)),
            //  null));
            //        }
            //    });

            //    //tasks[i] = Task.Run(async () =>
            //    //{
            //    //    for (int j = 0; j < 500; j++)
            //    //    {
            //    //       var data =  await eventStoreConnection.ReadStreamEventsForwardAsync("Account-Test", 0, 100, false);
            //    //    }
            //    //});
            //}

            //Task.WaitAll(tasks);
            Console.WriteLine("All done");

            Console.ReadLine();
        }
    }
}
