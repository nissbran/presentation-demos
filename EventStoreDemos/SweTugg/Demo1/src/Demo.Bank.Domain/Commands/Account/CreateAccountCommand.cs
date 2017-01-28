namespace Demo.Bank.Domain.Commands.Account
{
    public class CreateAccountCommand : ICommand
    {
        public string AccountNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string RegistrationNumber { get; set; }
    }
}