namespace EF7TestConsoleApp
{
    using System.Linq;
    using Bank.Domain.Models;
    using Bank.Repository.Context;
    using Bank.Repository.Mapping.Entities;

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new BankContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var customer = new Customer("Nisse", "Nissesson");
                context.Customers.Add(customer);
                context.Customers.Add(new Customer("Nisse", "Nissesson"));
                context.Customers.Add(new Customer("Nisse", "Nissesson"));

                context.Transactions.Add(new BankTransaction(customer, 100));

                context.SaveChanges();
            }

            using (var context = new BankContext())
            {
                var customers = context.Customers
                    .Where(customer => customer.FirstName == "Nisse")
                    .ToList();

                var transactions = context.Transactions.ToList();
                //customers

            }
        }
    }
}
