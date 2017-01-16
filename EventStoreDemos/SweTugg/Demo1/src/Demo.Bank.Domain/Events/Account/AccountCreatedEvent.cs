namespace Demo.Bank.Domain.Events.Account
{
    public class AccountCreatedEvent : DomainEvent
    {
        public string AccountNumber { get; set; }
    }
}