namespace Bank.Repository.Mappings.Customers
{
    using Domain.Models.Customer;
    using FluentNHibernate.Mapping;

    public class PrivatePersonMap : SubclassMap<PrivatePerson>
    {
        public PrivatePersonMap()
        {
            DiscriminatorValue("PrivatePerson");

            Map(person => person.FirstName);
            Map(person => person.LastName);
        }
    }
}
