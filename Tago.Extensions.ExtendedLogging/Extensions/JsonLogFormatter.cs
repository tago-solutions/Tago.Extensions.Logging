using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Tago.Extensions.ExtendedLogging;
using Tago.Extensions.Logging;

namespace Tago.Extensions.ExtendedLogging
{

    public class JsonLogFormatter : ILogEntryFormatter
    {
        public string FormatMessage(ILogMessageEntry entry)
        {
            //return $"{entry.Timestamp}\t{entry.Message}";

            if (!string.IsNullOrWhiteSpace(entry.Message))
            {
                //if (entry.Message.TrimStart().StartsWith("{"))
                //{
                //    return $"{entry.Message}";
                //}
                //else
                //{
                    return LoggingExtensions.GetJson(entry);
                    //return "{\"message\": \"" + entry.Message.Replace("\"", @"\\""") + "\"}";
                //}
            }

            return "";
        }
    }
}




//    public interface ITagoLogger : ILogger
//    {
//    }

//    public interface ITagoLogger<out T> : ITagoLogger
//    {
//    }

//    public class TagoLogger<T> : ITagoLogger<T>
//    {
//        private readonly ILogger _logger;

//        /// <summary>
//        /// Creates a new <see cref="Logger{T}"/>.
//        /// </summary>
//        /// <param name="factory">The factory.</param>
//        public TagoLogger(ILoggerFactory factory)
//        {
//            if (factory == null)
//            {
//                throw new ArgumentNullException(nameof(factory));
//            }

//            _logger = factory.CreateLogger(TypeNameHelper.GetTypeDisplayName(typeof(T)));
//        }

//        IDisposable ILogger.BeginScope<TState>(TState state)
//        {
//            return _logger.BeginScope(state);
//        }

//        bool ILogger.IsEnabled(LogLevel logLevel)
//        {
//            return _logger.IsEnabled(logLevel);
//        }

//        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
//        {
//            _logger.Log(logLevel, eventId, state, exception, formatter);
//        }
//    }
//}

