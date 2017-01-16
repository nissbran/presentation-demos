namespace Demo.Bank.Domain.Exceptions
{
    using System;
    using Commands;

    public class CommandExecutionException : Exception
    {
        public CommandExecutionException(Exception innerException) : base("The command failed!", innerException)
        {
        }

        public CommandExecutionException(ICommand command, Exception innerException) : base($"The command {command.GetType().Name} failed!", innerException)
        {
        }
    }
}