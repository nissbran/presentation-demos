namespace Demo.Bank.Domain.ReadModels
{
    using System.Collections.Generic;

    public class AccountBalanceReadModel
    {
        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public List<int> EventNumbersProcessed { get; set; } = new List<int>();
    }
}