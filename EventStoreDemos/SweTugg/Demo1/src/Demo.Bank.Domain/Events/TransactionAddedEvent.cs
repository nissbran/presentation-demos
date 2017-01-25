namespace Demo.Bank.Domain.Events
{
    public abstract class TransactionAddedEvent : DomainEvent
    {
        public decimal Amount { get; set; }
        
        public decimal Vat { get; set; }
    }
}