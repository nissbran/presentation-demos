namespace Demo.Bank.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Domain.Commands;
    using Domain.Commands.Account;
    using Microsoft.AspNetCore.Mvc;
    using Model.Transactions;

    public class AddTransactionController : Controller
    {
        private readonly ICommandMediator _commandMediator;

        public AddTransactionController(ICommandMediator commandMediator)
        {
            _commandMediator = commandMediator;
        }

        [HttpPost("account/{id}/transactions/card")]
        public async Task<IActionResult> AddCardTransaction([FromRoute]string id, [FromBody] AddCardTransactionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _commandMediator.Send(new AddCardTransactionToAccountCommand
            {
                AccountNumber = id,
                Amount = request.Amount,
                AuthCode = request.AuthCode,
            });

            return Ok();
        }
    }
}