namespace Demo.Bank.WebApi.Model.Transactions
{
    public class AddCardTransactionRequest
    {
        public decimal Amount { get; set; }

        public string AuthCode { get; set; }
    }
}