namespace Bank.Repository.Mapping.Entities
{
    using System.Data.Entity;
    using Domain.Models.Customer;
    using Infrastructure;
    using Interfaces;

    public class CustomerMap : IEntityMap
    {
        public void ConfigureModel(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BankCustomer>()
                .HasKey(customer => customer.CustomerId);

            modelBuilder.ComplexType<RegistrationNumber>();

            modelBuilder.Entity<BankCustomer>()
                .Map<PrivatePerson>(m => m.Requires("CustomerType").HasValue("PrivatePerson"))
                .Map<Company>(m => m.Requires("CustomerType").HasValue("Company"));
        }
    }
}
