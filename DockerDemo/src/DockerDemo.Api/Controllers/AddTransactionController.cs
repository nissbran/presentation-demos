namespace DockerDemo.Api.Controllers
{
    using System.Threading.Tasks;
    using Domain.Account;
    using Infrastructure.Repository;
    using Microsoft.AspNetCore.Mvc;
    using Model;

    public class AddTransactionContoller : Controller
    {
        private readonly IEventStore _eventStore;

        public AddTransactionContoller(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        [HttpPost("account/{accountNumber}/transactions")]
        public async Task<IActionResult> Post([FromRoute]string accountNumber, [FromBody]AddTransactionRequest request)
        {
            await _eventStore.AddEvent(new TransactionAddedEvent
            {
                AggregateRoot = accountNumber,
                Amount = request.Amount
            });

            return Ok();
        }
    }
}