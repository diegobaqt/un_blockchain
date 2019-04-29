using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace UNBlockchain.Extensions.ErrorLog
{
    internal class CustomLoggerProvider : ILoggerProvider
    {
        private readonly CustomLoggerProviderConfiguration _loggerConfig;
        private readonly ConcurrentDictionary<string, CustomLogger> _loggers =
            new ConcurrentDictionary<string, CustomLogger>();

        public CustomLoggerProvider(CustomLoggerProviderConfiguration config)
        {
            _loggerConfig = config;
        }

        public ILogger CreateLogger(string category)
        {
            return _loggers.GetOrAdd(category,
                name => new CustomLogger(name, _loggerConfig));
        }

        void IDisposable.Dispose()
        {
            //Write code here to dispose the resources
        }
    }
}
