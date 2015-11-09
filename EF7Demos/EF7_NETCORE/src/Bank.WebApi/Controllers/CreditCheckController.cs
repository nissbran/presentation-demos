using System;
using System.Linq;
using Bank.Domain.Models;
using Bank.Repository.Context;

namespace Bank.WebApi.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;

    [Route("api/[controller]")]
    public class CreditCheckController : Controller
    {
        private readonly BankContext _bankContext;

        public CreditCheckController(BankContext bankContext)
        {
            _bankContext = bankContext;
        }

        [HttpGet]
        public IEnumerable<CreditCheckResult> Get()
        {
            return _bankContext.CreditCheckResults.ToList();
        }
        
        [HttpGet("{id}")]
        public CreditCheckResult Get(Guid id)
        {
            return _bankContext.CreditCheckResults.FirstOrDefault(ccr => ccr.CreditCheckResultId == id);
        }
        
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
