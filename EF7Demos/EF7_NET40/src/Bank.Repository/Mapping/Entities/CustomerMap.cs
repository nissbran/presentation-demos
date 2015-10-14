namespace Bank.Repository.Mapping.Entities
{
    using Bank.Domain.Models;
    using Interfaces;
    using Microsoft.Data.Entity;

    public class CustomerMap : IEntityMap
    {
        public void ConfigureModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Customer>()
                .Key(customer => customer.CustomerId);
        }
    }
}
