namespace Demo.Bank.EventPublisher
{
    using System;
    using Configuration;
    using Domain.Models.Account;
    using EventStore.Lib.Common;
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

            eventStoreConnection.ConnectAsync().Wait();

            var eventStore = new EventStoreDomainRepository(eventStoreConnection);

            Account account;
            try
            {
                account = eventStore.GetById<Account>("test1234").Result;

                account.AddBankTransferTransaction(50);
                eventStore.Save(account).Wait();
            }
            catch (ArgumentException e)
            {
                account = new Account("test1234");
                eventStore.Save(account).Wait();
                Console.WriteLine(e);
            }

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
