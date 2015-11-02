namespace Bank.Domain.Models.Customers
{
    using System;

    public abstract class BankCustomer
    {
        public long CustomerId { get; protected set; }

        public DateTimeOffset CreatedOn { get; protected set; }

        //public RegistrationNumber RegistrationNumber { get; private set; }

        //protected BankCustomer(RegistrationNumber registrationNumber)
        //{
        //}

        protected BankCustomer() { }
    }
}
