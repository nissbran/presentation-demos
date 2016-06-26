using EventDomain.Model.Data;

namespace EventDomain.Model.Events
{
    public abstract class TransactionEvent : EventBase<Transaction, Metadata>
    {
    }
}