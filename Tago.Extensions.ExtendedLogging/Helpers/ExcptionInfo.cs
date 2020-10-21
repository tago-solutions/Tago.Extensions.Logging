using System;

namespace Tago.Extensions.ExtendedLogging
{
    public class ExceptionInfo
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }

        public static ExceptionInfo Create(Exception ex)
        {
            var res = new ExceptionInfo
            {
                //Id = ex
                Message = ex.Message,
                StackTrace = ex.StackTrace??ex.ToString(),
                Type = ex.GetType().FullName,
            };


            return res;
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

