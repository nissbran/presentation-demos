namespace Demo.Bank.Domain.Models.Account
{
    internal class AccountState
    {
        public AccountState()
        {
            ContactDetails = new AccountContactDetails();
        }

        public string AccountNumber { get; set; }

        public AccountContactDetails ContactDetails { get; set; }

        public decimal Balance { get; set; }
    }
}