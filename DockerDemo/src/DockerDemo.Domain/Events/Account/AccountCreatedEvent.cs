namespace DockerDemo.Domain.Account
{
    public class AccountCreatedEvent : AccountEvent
    {
        
        public string AccountNumber { get; set; }
    }
}