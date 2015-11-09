namespace EF7_TestConsoleApp.SQLite
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using Bank.Domain.Models.Customers;
    using Bank.Repository.Context;
    using Microsoft.Data.Entity;

    public class Program
    {
        public void Main(string[] args)
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlite("Filename=testdb.db");

            using (var context = new BankContext(builder.Options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 100; i++)
            {
                using (var context = new BankContext(builder.Options))
                {
                    context.Customers.Add(new PrivatePerson("Nisse", "Nissesson"));

                    context.SaveChanges();
                }
            }

            using (var context = new BankContext(builder.Options))
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
