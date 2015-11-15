using System;

namespace Bank.Domain.Models
{
    public class CreditCheckResult
    {
        public Guid CreditCheckResultId { get; protected set; }

        public decimal AllowedCreditAmount { get; private set; }
    
        public string Institute { get; private set; }

        public CreditCheckResult(decimal allowedCreditAmount, string institute)
        {
            AllowedCreditAmount = allowedCreditAmount;
            Institute = institute;
        }

        protected CreditCheckResult() { }
    }
}
