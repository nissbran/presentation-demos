namespace Bank.Domain.Models.Customer
{
    using Infrastructure;

    public class PrivatePerson : BankCustomer
    {
        public virtual string FirstName { get; protected set; }

        public virtual string LastName { get; protected set; }

        public PrivatePerson(string firstName, string lastName, RegistrationNumber registrationNumber)
            : base(registrationNumber)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        protected PrivatePerson() { }
    }
}
