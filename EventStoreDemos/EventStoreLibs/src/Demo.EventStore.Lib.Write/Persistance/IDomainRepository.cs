namespace Demo.EventStore.Lib.Write.Persistance
{
    using System.Threading.Tasks;
    using Common.Domain;

    public interface IDomainRepository
    {
        Task<TAggregateRoot> GetById<TAggregateRoot>(string aggregateRootId) where TAggregateRoot : IAggregateRoot, new();

        Task Save(IAggregateRoot aggregateRoot);
    }
}