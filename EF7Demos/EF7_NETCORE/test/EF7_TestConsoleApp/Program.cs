namespace EF7_TestConsoleApp
{
    using System;
    using System.Diagnostics;
    using Bank.Domain.Models.Customers;
    using Bank.Repository.Context;
    using System.Linq;

    public class Program
    {
        public void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            using (var context = new BankContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            for (int i = 0; i < 100; i++)
            {
                using (var context = new BankContext())
                {
                    var customer = new PrivatePerson("Nisse", "Nissesson");
                    context.Customers.Add(customer);
                    context.Customers.Add(new Company("MyCompany"));
                    context.Customers.Add(new PrivatePerson("Test","Testsson"));

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
