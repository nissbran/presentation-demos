namespace Demo.Bank.Domain.ReadModels
{
    public class AccountBalanceReadModel
    {
        public int LastEventNumber { get; set; }

        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }
    }
}