namespace Bank.Domain.Models.Customer
{
    using Infrastructure;

    public abstract class BankCustomer
    {
        public virtual long CustomerId { get; protected set; }

        public virtual RegistrationNumber RegistrationNumber { get; protected set; }

        protected BankCustomer(RegistrationNumber registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }

        protected BankCustomer() { }
    }
}
