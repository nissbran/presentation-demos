namespace Bank.Domain.Models
{
    public class Customer
    {
        public long CustomerId { get; protected set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        protected Customer() { }
    }
}
