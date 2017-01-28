namespace Demo.Bank.WebApi.Controllers
{
    using Cache;
    using Domain.ReadModels;
    using Microsoft.AspNetCore.Mvc;
    using Model.Account;

    public class GetAccountBalanceController : Controller
    {
        private readonly IRedisRepository _redisRepository;

        public GetAccountBalanceController(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }

        [HttpGet("account/{id}/balance")]
        public IActionResult Get([FromRoute]string id)
        {
            var balance = _redisRepository.Get<AccountBalanceReadModel>($"balance-{id}");

            if (string.IsNullOrWhiteSpace(balance.AccountNumber))
                return NotFound();

            return Ok(new AccountBalanceResponse
            {
                AccountNumber = balance.AccountNumber,
                Balance = balance.Balance
            });
        }
    }
}