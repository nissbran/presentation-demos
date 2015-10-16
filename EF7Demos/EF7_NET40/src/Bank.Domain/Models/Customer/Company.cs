namespace Bank.Domain.Models.Customer
{
    using Infrastructure;

    public class Company : BankCustomer
    {
        public string Name { get; private set; }

        public Company(string name, RegistrationNumber number) 
            : base(number)
        {
            Name = name;
        }

        protected Company() { }
    }
}
