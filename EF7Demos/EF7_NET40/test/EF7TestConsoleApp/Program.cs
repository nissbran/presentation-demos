namespace EF7TestConsoleApp
{
    using System.Linq;
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
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            for (int i = 0; i < 10; i++)
            {
                using (var context = new BankContext())
                {
                    var customer = fixture.Create<PrivatePerson>();
                    context.Customers.Add(customer);
                    context.Customers.Add(fixture.Create<Company>());
                    context.Customers.Add(fixture.Create<PrivatePerson>());
                    
                    context.SaveChanges();
                }
            }

            using (var context = new BankContext())
            {
                var customers = context.Customers
                    .Where(customer => customer.RegistrationNumber == "test")
                    .ToList();

                var transactions = context.Transactions.ToList();
                //customers

            }
        }
    }
}
