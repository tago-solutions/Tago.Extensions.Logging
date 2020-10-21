using Microsoft.Extensions.Logging;
using System;
using Tago.Extensions.Logging.Abstractions;

namespace Tago.Extensions.ExtendedLogging
{
    public class LogEnrtyExFactory : ILogMessageEntryFactory
    {
        public ILogMessageEntry Create(LogLevel logLevel)
        {
            return Create(logLevel, null);
        }

        public ILogMessageEntry Create(LogLevel logLevel, string message)
        {
            return new LogEnrtyEx(logLevel, message);
        }

        public ILogMessageEntry Create(DateTime now, LogLevel logLevel, string logName, string message, int eventId, string correlationid, Exception ex = null)
        {
            return new LogEnrtyEx(logLevel, message)
            {
                Timestamp = now,
                Category = logName,
                EventId = eventId,
                CorrelationId = correlationid,
                Exception = ex,
            };
        }
    }    
}