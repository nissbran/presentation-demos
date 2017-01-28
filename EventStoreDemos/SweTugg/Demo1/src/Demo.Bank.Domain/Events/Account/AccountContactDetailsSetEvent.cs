namespace Demo.Bank.Domain.Events.Account
{
    public class AccountContactDetailsSetEvent : DomainEvent
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string RegistrationNumber { get; set; }
    }
}