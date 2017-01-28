namespace Demo.Bank.Sql.Model
{
    public class AccountInformation
    {
        public long AccountInformationId { get; set; }

        public string AccountNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string RegistrationNumber { get; set; }

        public decimal Balance { get; set; }

        public int Version { get; set; }
    }
}