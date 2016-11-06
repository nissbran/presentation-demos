namespace DockerDemo.Domain.Account
{
    public class TransactionAddedEvent : AccountEvent
    {
        public decimal Amount { get; set; }
    }
}