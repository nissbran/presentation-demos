namespace Bank.Domain.Models
{
    using System;
    using Customer;

    public class BankTransaction
    {
        public virtual Guid BankTransactionId { get; protected set; }

        public virtual BankCustomer Customer { get; protected set; }

        public virtual decimal Amount { get; protected set; }

        public BankTransaction(BankCustomer customer, decimal amount)
        {
            Customer = customer; 
            Amount = amount;
        }

        protected BankTransaction() { }
    }
}
