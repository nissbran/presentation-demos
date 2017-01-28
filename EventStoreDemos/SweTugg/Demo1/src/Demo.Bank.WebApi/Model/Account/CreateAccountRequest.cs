namespace Demo.Bank.WebApi.Model.Account
{
    using System.ComponentModel.DataAnnotations;

    public class CreateAccountRequest
    {
        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }
    }
}