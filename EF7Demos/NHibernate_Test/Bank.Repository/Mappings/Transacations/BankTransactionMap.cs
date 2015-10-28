namespace Bank.Repository.Mappings.Transacations
{
    using Domain.Models;
    using FluentNHibernate.Mapping;

    internal sealed class BankTransactionMap : ClassMap<BankTransaction>
    {
        public BankTransactionMap()
        {
            Table("BankTransactions");
            Id(t => t.BankTransactionId).Not.Nullable().GeneratedBy.GuidComb();

            Map(t => t.Amount);

            References(x => x.Customer, "CustomerId").Not.Nullable();
        }
    }
}
