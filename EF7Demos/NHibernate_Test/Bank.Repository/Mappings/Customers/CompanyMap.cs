namespace Bank.Repository.Mappings.Customers
{
    using Domain.Models.Customer;
    using FluentNHibernate.Mapping;

    public class CompanyMap : SubclassMap<Company>
    {
        public CompanyMap()
        {
            DiscriminatorValue("Company");

            Map(company => company.Name);
        }
    }
}
