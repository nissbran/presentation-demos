namespace Test_NHibernate_ConsoleApp
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using Bank.Domain.Models;
    using Bank.Domain.Models.Customer;
    using Bank.Repository.Session;
    using Ploeh.AutoFixture;

    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["BankContext"].ConnectionString;
            SessionFactory.Initialize(connectionString);
            using (var session = SessionFactory.OpenSession())
            {
                var transactions = session.CreateCriteria(typeof(BankTransaction)).List<BankTransaction>();
                foreach (var bankTransaction in transactions)
                {
                    session.Delete(bankTransaction);
                }

                var customers = session.CreateCriteria(typeof(BankCustomer)).List<BankCustomer>();
                foreach (var customer in customers)
                {
                    session.Delete(customer);
                }

                session.Flush();
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var fixture = new Fixture();
            var privatePerson = fixture.Create<PrivatePerson>();
            using (var session = SessionFactory.OpenSession())
            {
                session.Save(privatePerson);
                session.Flush();
            }
            fixture.Inject<BankCustomer>(privatePerson);

            for (int i = 0; i < 100; i++)
            {
                using (var session = SessionFactory.OpenSession())
                {
                    var customer = fixture.Create<PrivatePerson>();

                    session.Save(customer);
                    session.Save(fixture.Create<Company>());
                    session.Save(fixture.Create<PrivatePerson>());

                    session.Flush();
                }

                using (var session = SessionFactory.OpenSession())
                {
                    for (int y = 0; y < 100; y++)
                    {
                        session.Save(fixture.Create<BankTransaction>());
                        session.Save(fixture.Create<BankTransaction>());
                        session.Save(fixture.Create<BankTransaction>());
                        session.Save(fixture.Create<BankTransaction>());
                    }

                    session.Flush();
                }
            }

            stopwatch.Stop();
            Console.WriteLine("Elapsed: {0}", stopwatch.Elapsed);

            using (var session = SessionFactory.OpenSession())
            {
                var customers = session.CreateCriteria(typeof(BankCustomer)).List<BankCustomer>();

                var test = session.CreateCriteria(typeof (BankTransaction)).List<BankTransaction>();
            }

            Console.ReadKey();
        }
    }
}
