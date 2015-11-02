namespace EF6_TestConsoleApplication
{
    using System;
    using System.Diagnostics;
    using Bank.Domain.Models;
    using Bank.Domain.Models.Customer;
    using Bank.Repository.Context;
    using Ploeh.AutoFixture;

    class Program
    {
        static void Main(string[] args)
        {

            var fixture = new Fixture();
            using (var context = new BankContext())
            {
                if (!context.Database.CreateIfNotExists())
                {
                    context.Database.Delete();
                    context.Database.CreateIfNotExists();
                }
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var privatePerson = fixture.Create<PrivatePerson>();
            using (var context = new BankContext())
            {
                context.Customers.Add(privatePerson);
                context.SaveChanges();
            }

            fixture.Inject<BankCustomer>(privatePerson);

            for (int i = 0; i < 100; i++)
            {
                using (var context = new BankContext())
                {
                    var customer = fixture.Create<PrivatePerson>();

                    context.Customers.Add(customer);
                    context.Customers.Add(fixture.Create<Company>());
                    context.Customers.Add(fixture.Create<PrivatePerson>());

                    context.SaveChanges();
                }

                using (var context = new BankContext())
                {
                    context.Customers.Attach(privatePerson);
                    for (int y = 0; y < 100; y++)
                    {
                        context.Transactions.Add(fixture.Create<BankTransaction>());
                        context.Transactions.Add(fixture.Create<BankTransaction>());
                        context.Transactions.Add(fixture.Create<BankTransaction>());
                        context.Transactions.Add(fixture.Create<BankTransaction>());
                    }

                    context.SaveChanges();
                }
            }

            //using (var context = new BankContext())
            //{
            //    var customers = context.Customers.ToList();

            //    var transactions = context.Transactions.ToList();
            //}

            stopwatch.Stop();
            Console.WriteLine("Elapsed: {0}", stopwatch.Elapsed);

            Console.ReadLine();
        }
    }
}
