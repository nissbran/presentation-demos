namespace DockerDemo.Api.Controllers
{
    using System.Threading.Tasks;
    using Domain.Account;
    using Infrastructure.Repository;
    using Microsoft.AspNetCore.Mvc;
    using Model;

    public class CreateAccountController : Controller
    {
        private readonly IEventStore _eventStore;

        public CreateAccountController(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        [HttpPost("account")]
        public async Task<IActionResult> Post([FromBody]CreateAccountRequest request)
        {
            await _eventStore.AddEvent(new AccountCreatedEvent
            {
                AggregateRoot = request.AccountNumber,
                AccountNumber = request.AccountNumber
            });

            return Ok();
        }
    }
}