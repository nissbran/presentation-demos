namespace Bank.WebApi.Controllers
{
    using Microsoft.AspNet.Mvc;

    [Route("api/[controller]")]
    public class BankTransactionsController : Controller
    {
        //private BankContext DataContext { get; }

        //public BankTransactionsController(BankContext dataContext)
        //{
        //    DataContext = dataContext;
        //}

        //[HttpGet]
        //public IEnumerable<BankTransaction> Get()
        //{
        //    return DataContext.Transactions
        //                      .Include(transaction => transaction.Customer)
        //                      .ToList();
        //}

        //[HttpGet("{id}")]
        //public BankTransaction Get(Guid id)
        //{
        //    return DataContext.Transactions.First(transaction => transaction.BankTransactionId == id);
        //}

        //[HttpPost]
        //public void Post([FromBody]BankTransaction transaction)
        //{
        //    DataContext.Add(transaction);
            
        //    DataContext.SaveChanges();
        //}
    }
}
