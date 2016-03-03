namespace EF7_TestConsoleApp_SQLServer
{
    using System;
    using System.Diagnostics;
    using Bank.Domain.Models;
    using Bank.Domain.Models.Customers;
    using Bank.Repository.Context;
    using Bank.Repository.SQLServer.Context;
    using Microsoft.Data.Entity;

    public class Program
    {
        public void Main(string[] args)
        {
            var connectionString = "Data Source = .; Initial Catalog = EF7CoreContext; Integrated Security = True;";
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(connectionString);

            using (var context = new SqlServerMigrationContext(builder.Options))
            {
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }

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

                    context.Customers.AddRange(customer,
                        new PrivatePerson("Nisse2", "Mannen"),
                        new PrivatePerson("Nisse2", "Mannen"));

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
