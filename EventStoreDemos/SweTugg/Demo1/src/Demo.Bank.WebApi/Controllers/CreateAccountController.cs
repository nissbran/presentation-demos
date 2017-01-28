namespace Demo.Bank.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Domain.Commands;
    using Domain.Commands.Account;
    using Microsoft.AspNetCore.Mvc;
    using Model.Account;

    public class CreateAccountController : Controller
    {
        private readonly ICommandMediator _commandMediator;

        public CreateAccountController(ICommandMediator commandMediator)
        {
            _commandMediator = commandMediator;
        }

        [HttpPost("account")]
        public async Task<IActionResult> CreateAccount([FromBody]CreateAccountRequest createAccountRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _commandMediator.Send(new CreateAccountCommand
            {
                AccountNumber = createAccountRequest.AccountNumber,
                FirstName = createAccountRequest.FirstName,
                LastName = createAccountRequest.LastName,
                RegistrationNumber = createAccountRequest.LastName
            });

            return Ok();
        }
    }
}
