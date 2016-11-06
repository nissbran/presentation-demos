namespace DockerDemo.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AlwaysOnController : Controller
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok("It's on");
        }
    }
}