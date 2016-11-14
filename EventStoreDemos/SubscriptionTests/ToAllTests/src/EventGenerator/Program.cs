using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EventDomain.Model.Data;
using EventDomain.Model.Events;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Ploeh.AutoFixture;

namespace EventGenerator
{
    using EventStore.ClientAPI.SystemData;

    public class Program
    {
        private const int NumberOfThreads = 1;
        private const int NumberOfIterations = 30000;

        private static readonly Fixture _Fixture = new Fixture();

        public static void Main(string[] args)
        {
            var connection = EventStoreConnection.Create(new Uri("tcp://192.168.99.100:1113"));

            connection.ConnectAsync().Wait();

            while (true)
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var tasks = new Task[NumberOfThreads];
                for (int i = 0; i < NumberOfThreads; i++)
                {
                    tasks[i] = Task.Run(async () =>
                    {
                        var events = new List<EventData>();
                        for (int j = 0; j < NumberOfIterations; j++)
                        {
                            var purchase = _Fixture.Create<PurchaseEvent>();
                            purchase.MetaData.Created = DateTimeOffset.Now;

                            events.Add(new EventData(Guid.NewGuid(),
                                typeof(Purchase).Name, true,
                                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(purchase.Data)),
                                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(purchase.MetaData))));

                            if (j != 0 && j % 200 == 0)
                            {
                                var result = await connection.AppendToStreamAsync($"Transactions", ExpectedVersion.Any, events, new UserCredentials("admin", "changeit"));
                                events.Clear();
                            }
                        }
                    });
                }

                Task.WaitAll(tasks);
                stopWatch.Stop();

                Console.WriteLine($"Inserting {NumberOfThreads * NumberOfIterations} events, total time: {stopWatch.ElapsedMilliseconds} ms");
                break;
            }
            Console.ReadLine();
        }
    }
}
