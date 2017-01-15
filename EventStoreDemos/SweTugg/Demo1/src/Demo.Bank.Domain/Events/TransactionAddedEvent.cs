// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransactionAddedEvent.cs" company="Collector AB">
//   Copyright © Collector AB. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Demo.Bank.Domain.Events
{
    public abstract class TransactionAddedEvent : DomainEvent
    {
        public decimal Amount { get; set; }
        
        public decimal Vat { get; set; }
    }
}