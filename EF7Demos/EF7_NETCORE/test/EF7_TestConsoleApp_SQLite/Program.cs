namespace EF7_TestConsoleApp
{
    using System;
    using System.Diagnostics;
    using Bank.Domain.Models.Customers;
    using System.Linq;
    using Bank.Repository.SQLite.Context;

    public class Program
    {
        public void Main(string[] args)
        {
            using (var context = new BankContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 100; i++)
            {
                using (var context = new BankContext())
                {
                    context.Customers.Add(new PrivatePerson("Nisse", "Nissesson"));

                    context.SaveChanges();
                }
            }

            using (var context = new BankContext())
            {
                var customers = context.Customers.ToList();

                var transactions = context.Transactions.ToList();
            }

            stopwatch.Stop();
            Console.WriteLine("Elapsed: {0}", stopwatch.Elapsed);

            Console.ReadLine();
        }
    }
}
