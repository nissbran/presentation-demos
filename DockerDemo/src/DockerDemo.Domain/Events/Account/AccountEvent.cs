namespace DockerDemo.Domain.Account
{
    using Newtonsoft.Json;
    
    public abstract class AccountEvent : Event
    {
        [JsonIgnore]
        public override string DomainType { get; } = "Account";
    }
}