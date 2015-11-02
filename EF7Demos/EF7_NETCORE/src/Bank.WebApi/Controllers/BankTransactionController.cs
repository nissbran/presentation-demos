namespace Bank.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Data.Entity;
    using Repository.SQLite.Context;

    [Route("api/[controller]")]
    public class BankTransactionsController : Controller
    {
        private BankContext DataContext { get; }

        public BankTransactionsController(BankContext dataContext)
        {
            DataContext = dataContext;
        }

        [HttpGet]
        public IEnumerable<BankTransaction> Get()
        {
            return DataContext.Transactions.Include(transaction => transaction.Customer).ToList();
        }

        [HttpGet("{id}")]
        public BankTransaction Get(Guid id)
        {
            return DataContext.Transactions.First(transaction => transaction.BankTransactionId == id);
        }

        [HttpPost]
        public void Post([FromBody]BankTransaction transaction)
        {
            DataContext.Add(transaction);
            
            DataContext.SaveChanges();
        }

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
