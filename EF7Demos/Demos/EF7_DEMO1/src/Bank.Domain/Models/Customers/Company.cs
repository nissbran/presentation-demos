namespace Bank.Domain.Models.Customers
{
    using System;

    public class Company : BankCustomer
    {
        public string Name { get; private set; }

        public Company(string name)
        {
            Name = name;
            CreatedOn = DateTime.Now;
        }

        protected Company() { }
    }
}
