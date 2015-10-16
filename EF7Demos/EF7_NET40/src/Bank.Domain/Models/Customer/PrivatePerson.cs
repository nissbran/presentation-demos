namespace Bank.Domain.Models.Customer
{
    using Infrastructure;

    public class PrivatePerson : BankCustomer
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public PrivatePerson(string firstName, string lastName, RegistrationNumber registrationNumber) :
            base(registrationNumber)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        protected PrivatePerson() { }
    }
}
