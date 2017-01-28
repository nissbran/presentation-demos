namespace Demo.Bank.WebApi.Model.Account
{
    public class AccountInformationResponse
    {
        public string AccountNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string RegistrationNumber { get; set; }

        public decimal Balance { get; set; }
    }
}