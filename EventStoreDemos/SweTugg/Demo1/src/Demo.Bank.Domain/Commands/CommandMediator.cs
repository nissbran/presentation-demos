namespace Demo.Bank.Domain.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CommandMediator : ICommandMediator
    {
        private readonly Dictionary<Type, ICommandHandler> _commandHandlers;

        public CommandMediator(IEnumerable<ICommandHandler> commandHandlers)
        {
            _commandHandlers = new Dictionary<Type, ICommandHandler>();

            foreach (var commandHandler in commandHandlers)
            {
                _commandHandlers.Add(commandHandler.CommandType, commandHandler);
            }
        }

        public async Task Send(ICommand command)
        {
            var commandHandler = GetCommandHandlerFor(command.GetType());
            try
            {
                await commandHandler.Handle(command);
            }
            catch (Exception x)
            {
                throw new CommandExecutionException(command, x);
            }
        }

        private ICommandHandler GetCommandHandlerFor(Type commandType)
        {
            ICommandHandler commandHandler;
            if (!_commandHandlers.TryGetValue(commandType, out commandHandler))
                throw new ArgumentException($"Command handler for {commandType.Name} could not be found.");

            return commandHandler;
        }
    }
}