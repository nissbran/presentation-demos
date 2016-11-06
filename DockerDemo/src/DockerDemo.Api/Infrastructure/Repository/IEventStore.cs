namespace DockerDemo.Infrastructure.Repository
{
    using System.Threading.Tasks;
    using DockerDemo.Domain;

    public interface IEventStore
    {
        Task AddEvent(Event domainEvent);
    }
}