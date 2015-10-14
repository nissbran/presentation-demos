namespace Bank.Domain.Models
{
    using System;

    public class BankTransaction
    {
        public Guid BankTransactionId { get; protected set; }

        public Customer Customer { get; private set; }

        public decimal Amount { get; private set; }

        public BankTransaction(Customer customer, decimal amount)
        {
            Customer = customer;
            Amount = amount;
        }

        protected BankTransaction() { }
    }
}
