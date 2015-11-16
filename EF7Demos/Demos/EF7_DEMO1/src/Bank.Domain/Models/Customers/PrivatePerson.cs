namespace Bank.Domain.Models.Customers
{
    using System;

    public class PrivatePerson : BankCustomer
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public PrivatePerson(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            CreatedOn = DateTime.Now;
        }

        protected PrivatePerson() { }
    }
}
