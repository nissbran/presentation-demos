namespace Bank.Domain.Models.Customer
{
    using Infrastructure;

    public abstract class BankCustomer
    {
        public long CustomerId { get; protected set; }

        public RegistrationNumber RegistrationNumber { get; private set; }

        protected BankCustomer(RegistrationNumber registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }

        protected BankCustomer() { }
    }
}
