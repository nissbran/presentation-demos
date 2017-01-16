namespace Demo.Bank.Domain.Commands
{
    using System.Threading.Tasks;

    public interface ICommandMediator
    {
        Task Send(ICommand command);
    }
}