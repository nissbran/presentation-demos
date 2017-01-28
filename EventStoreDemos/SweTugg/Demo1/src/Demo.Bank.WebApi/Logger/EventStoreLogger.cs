namespace Demo.Bank.WebApi.Logger
{
    using System;
    using Microsoft.Extensions.Logging;

    public class EventStoreLogger : global::EventStore.ClientAPI.ILogger
    {
        private readonly ILogger _logger;

        public EventStoreLogger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EventStoreLogger>();
        }

        public void Error(string format, params object[] args)
        {
            _logger.LogError(string.Format(format, args));
        }

        public void Error(Exception ex, string format, params object[] args)
        {
            _logger.LogError(string.Format(format, args));
        }

        public void Info(string format, params object[] args)
        {
            _logger.LogInformation(string.Format(format, args));
        }

        public void Info(Exception ex, string format, params object[] args)
        {
            _logger.LogInformation(string.Format(format, args));
        }

        public void Debug(string format, params object[] args)
        {
            _logger.LogDebug(string.Format(format, args));
        }

        public void Debug(Exception ex, string format, params object[] args)
        {
            _logger.LogDebug(string.Format(format, args));
        }
    }
}
