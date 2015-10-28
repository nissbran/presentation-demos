namespace EF7TestConsoleApp
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.Linq;
    using Bank.Domain.Models;
    using Bank.Domain.Models.Customer;
    using Bank.Repository.Context;
    using Microsoft.Data.Entity;
    using Ploeh.AutoFixture;

    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["EFMigrationDb"].ConnectionString;
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(connectionString);

            using (var context = new BankContext(builder.Options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            //var stopwatch = new Stopwatch();
            //stopwatch.Start();

            //var fixture = new Fixture();
            //var privatePerson = fixture.Create<PrivatePerson>();
            //using (var context = new BankContext(builder.Options))
            //{
            //    context.Customers.Add(privatePerson);
            //    context.SaveChanges();
            //}

            //fixture.Inject<BankCustomer>(privatePerson);

            //for (int i = 0; i < 100; i++)
            //{
            //    using (var context = new BankContext(builder.Options))
            //    {
            //        var customer = fixture.Create<PrivatePerson>();

            //        context.Customers.Add(customer);
            //        //context.Customers.Add(fixture.Create<Company>());
            //        context.Customers.Add(fixture.Create<PrivatePerson>());
            //        context.Customers.Add(fixture.Create<PrivatePerson>());

            //        context.SaveChanges();
            //    }

            //    using (var context = new BankContext(builder.Options))
            //    {
            //        for (int y = 0; y < 100; y++)
            //        {
            //            context.Transactions.Add(fixture.Create<BankTransaction>());
            //            context.Transactions.Add(fixture.Create<BankTransaction>());
            //            context.Transactions.Add(fixture.Create<BankTransaction>());
            //            context.Transactions.Add(fixture.Create<BankTransaction>());
            //        }
            //        context.SaveChanges();
            //    }
            //}

            //using (var context = new BankContext())
            //{
            //    var customers = context.Customers.ToList();

            //    var transactions = context.Transactions.ToList();
            //}

            //stopwatch.Stop();
            //Console.WriteLine("Elapsed: {0}", stopwatch.Elapsed);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var privatePerson = new PrivatePerson("Nisse", "Nissessons");
            using (var context = new BankContext(builder.Options))
            {
                context.Customers.Add(privatePerson);
                context.SaveChanges();
            }

            for (int i = 0; i < 100; i++)
            {
                using (var context = new BankContext(builder.Options))
                {
                    var customer = new PrivatePerson("Nisse", "Nissessons");

                    context.Customers.Add(customer);
                    //context.Customers.Add(fixture.Create<Company>());
                    context.Customers.Add(new PrivatePerson("Nisse2", "Mannen"));
                    context.Customers.Add(new PrivatePerson("Nisse2", "Mannen"));

                    context.SaveChanges();
                }

                using (var context = new BankContext(builder.Options))
                {
                    for (int y = 0; y < 100; y++)
                    {
                        context.Transactions.Add(new BankTransaction(privatePerson, 34));
                        context.Transactions.Add(new BankTransaction(privatePerson, 35));
                        context.Transactions.Add(new BankTransaction(privatePerson, 36));
                        context.Transactions.Add(new BankTransaction(privatePerson, 37));
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
