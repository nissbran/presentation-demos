namespace Demo.Bank.Domain.Commands.Account
{
    public class AddCardTransactionToAccountCommand : ICommand
    {
        public string AccountNumber { get; set; }
        
        public decimal Amount { get; set; }

        public string AuthCode { get; set; }
    }
}