namespace Demo.EventStore.Lib.Common.Domain
{
    using System.Threading.Tasks;

    public interface IDomainRepository
    {
        Task<TAggregateRoot> GetById<TAggregateRoot>(string aggregateRootId) where TAggregateRoot : IAggregateRoot, new();

        Task Save(IAggregateRoot aggregateRoot);
    }
}