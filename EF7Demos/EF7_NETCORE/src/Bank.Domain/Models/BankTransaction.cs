namespace Bank.Domain.Models
{
    using System;
    using Customers;

    public class BankTransaction
    {
        public Guid BankTransactionId { get; protected set; }

        public BankCustomer Customer { get; private set; }

        public decimal Amount { get; private set; }

        public BankTransaction(BankCustomer customer, decimal amount)
        {
            Customer = customer;
            Amount = amount;
        }

        protected BankTransaction() { }
    }
}
