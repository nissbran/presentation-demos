namespace Demo.Bank.Domain.Commands
{
    using System;
    using System.Threading.Tasks;

    public interface ICommandHandler
    {
        Type CommandType { get; }

        Task Handle(ICommand command);
    }

    public interface ICommandHandler<in T> : ICommandHandler
        where T : ICommand
    {
        Task Handle(T command);
    }
}