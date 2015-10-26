namespace Bank.Domain.Models.Customer
{
    public class PrivatePerson : BankCustomer
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public PrivatePerson(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        protected PrivatePerson() { }
    }
}
