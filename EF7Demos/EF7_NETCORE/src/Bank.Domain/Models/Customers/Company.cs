namespace Bank.Domain.Models.Customers
{
    public class Company : BankCustomer
    {
        public string Name { get; private set; }

        public Company(string name)
        {
            Name = name;
        }

        protected Company() { }
    }
}
