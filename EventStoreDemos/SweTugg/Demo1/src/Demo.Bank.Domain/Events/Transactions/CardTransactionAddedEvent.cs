namespace Demo.Bank.Domain.Events.Transactions
{
    public class CardTransactionAddedEvent : TransactionAddedEvent
    {
        public string AuthCode { get; set; }
    }
}