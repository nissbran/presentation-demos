namespace Bank.Repository.Mapping.Entities
{
    using Domain.Models.Customers;
    using Interfaces;
    using Microsoft.Data.Entity;

    public class CustomerMap : IEntityMap
    {
        public void ConfigureModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BankCustomer>()
                .HasKey(customer => customer.CustomerId);

            modelBuilder.Entity<BankCustomer>().HasDiscriminator("CustomerType", typeof(string));

            modelBuilder.Entity<PrivatePerson>().HasBaseType<BankCustomer>();
            modelBuilder.Entity<Company>().HasBaseType<BankCustomer>();

            //modelBuilder.Entity<BankCustomer>().Discriminator("CustomerType", typeof(int))
            //    .HasValue(typeof(PrivatePerson), 1)
            //    .HasValue(typeof(Company), 2);
        }
    }
}
