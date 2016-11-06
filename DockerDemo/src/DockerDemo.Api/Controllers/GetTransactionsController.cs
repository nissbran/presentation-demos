namespace DockerDemo.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Model;

    public class GetTransactionsController : Controller
    {
        [HttpGet("account/{accountNumber}")]
        public IActionResult Post([FromRoute]string accountNumber)
        {
            return Ok();
        }
    }
}