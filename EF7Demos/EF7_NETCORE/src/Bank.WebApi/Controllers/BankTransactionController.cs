namespace Bank.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Data.Entity;
    using Repository.SQLServer.Context;

    [Route("api/[controller]")]
    public class BankTransactionsController : Controller
    {
        private BankContext DataContext { get; }

        public BankTransactionsController(BankContext dataContext)
        {
            DataContext = dataContext;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<BankTransaction> Get()
        {
            return DataContext.Transactions.Include(transaction => transaction.Customer).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public BankTransaction Get(Guid id)
        {
            return DataContext.Transactions.First(transaction => transaction.BankTransactionId == id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
