namespace Bank.Repository.Mappings.Customers
{
    using Domain.Models.Customer;
    using FluentNHibernate.Mapping;

    public class BankCustomerMap : ClassMap<BankCustomer>
    {
        public BankCustomerMap()
        {
            Table("BankCustomers");
            Id(customer => customer.CustomerId);
            DiscriminateSubClassesOnColumn("CustomerType");

            Component(customer => customer.RegistrationNumber, c => c.Map(x => x.Value, "RegistrationNumber"));
        }
    }
}
