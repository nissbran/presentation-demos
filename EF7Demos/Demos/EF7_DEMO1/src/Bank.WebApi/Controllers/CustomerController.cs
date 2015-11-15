namespace Bank.WebApi.Controllers
{
    using Microsoft.AspNet.Mvc;

    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        //private readonly BankContext _bankContext;

        //public CustomerController(BankContext bankContext)
        //{
        //    _bankContext = bankContext;
        //}

        //[HttpGet]
        //public IEnumerable<BankCustomer> Get()
        //{
        //    //return _bankContext.Customers.ToList();
        //}
        
        //[HttpGet("{id}")]
        //public BankCustomer Get(long id)
        //{
        //    //return _bankContext.Customers.FirstOrDefault(customer => customer.CustomerId == id);
        //}
    }
}
