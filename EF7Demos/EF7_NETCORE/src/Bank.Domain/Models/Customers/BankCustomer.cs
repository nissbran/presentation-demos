namespace Bank.Domain.Models.Customers
{
    public abstract class BankCustomer
    {
        public long CustomerId { get; protected set; }

        //public RegistrationNumber RegistrationNumber { get; private set; }

        //protected BankCustomer(RegistrationNumber registrationNumber)
        //{
        //}

        protected BankCustomer() { }
    }
}
