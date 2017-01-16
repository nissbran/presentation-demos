namespace Demo.Bank.Domain.Commands
{
    using System;
    using System.Threading.Tasks;

    public abstract class CommandHandler<T> : ICommandHandler<T>
       where T : ICommand
    {
        public Type CommandType { get; } = typeof(T);

        public async Task Handle(ICommand command)
        {
            await Handle((T)command);
        }

        public abstract Task Handle(T command);
    }
}