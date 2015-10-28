namespace Bank.Domain.Models.Customer
{
    public class Company : BankCustomer
    {
        public virtual string Name { get; protected set; }

        public Company(string name)
        {
            Name = name;
        }

        protected Company() { }
    }
}
