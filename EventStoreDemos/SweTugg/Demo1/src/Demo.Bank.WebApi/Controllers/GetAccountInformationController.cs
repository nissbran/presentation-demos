namespace Demo.Bank.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Model.Account;
    using Sql.Context;

    public class GetAccountInformationController : Controller
    {
        private readonly AccountInformationContext _accountInformationContext;

        public GetAccountInformationController(AccountInformationContext accountInformationContext)
        {
            _accountInformationContext = accountInformationContext;
        }

        [HttpGet("account/{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var information = await _accountInformationContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == id);

            if (information == null)
                return NotFound();

            return Ok(new AccountInformationResponse
            {
                AccountNumber = information.AccountNumber,
                RegistrationNumber = information.RegistrationNumber,
                FirstName = information.FirstName,
                LastName = information.LastName,
                Balance = information.Balance,
            });
        }

    }
}