namespace Bank.WebApi.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using Bank.Domain.Models.Customers;

    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        [HttpGet]
        public IEnumerable<BankCustomer> Get()
        {
            return new List<BankCustomer>();
        }

        [HttpGet("{id}")]
        public BankCustomer Get(long id)
        {
            return new Company("Test");
        }
    }
}
