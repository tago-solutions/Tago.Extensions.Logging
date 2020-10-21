using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tago.Extensions.ExtendedLogging
{
    public class BaseHttpRequestInfo
    {
        public string TraceIdentifier { get; set; }
        public string Uri { get; set; }
        public string UserId { get; set; }
        public string Scheme { get; set; }
        public string RemoteIpAddress { get; set; }
        public string ContentType { get; set; }
        public long? ContentLength { get; set; }
        public Dictionary<string, string> Header { get; set; }
        public HttpContent Body { get; set; }


        protected static Dictionary<string, string> HeaderToDictionary(IHeaderDictionary dic)
        {
            var dicHeaders = new Dictionary<string, string>();
            if (dic != null)
            {
                foreach (var hv in dic)
                {
                    dicHeaders.Add(hv.Key, string.Join(", ", hv.Value));
                }
            }

            return dicHeaders;
        }

        protected static async Task<object> ReadStreamInChuncksAsync(Stream stream, int? maxLength = null)
        {
            if (stream == null || stream?.Length == 0)
            {
                return "";
            }

            string hasMore = maxLength.HasValue ? (stream.Length > maxLength.Value ? "..." : "") : "";

            int readChunkBufferength = 4096;
            if (maxLength.HasValue && maxLength < readChunkBufferength)
                readChunkBufferength = maxLength.Value;

            stream.Seek(0, SeekOrigin.Begin);

            using (var textwriter = new StringWriter())
            {
                using (var reader = new System.IO.StreamReader(stream, System.Text.Encoding.Default, true, 1024, true))
                {
                    var readChunk = new char[readChunkBufferength];
                    int readChunkLength = 0;
                    int alreadyRead = 0;
                    do
                    {
                        readChunkLength = await reader.ReadBlockAsync(readChunk, 0, readChunkBufferength);
                        alreadyRead += readChunkLength;

                        await textwriter.WriteAsync(readChunk, 0, readChunkLength);

                        if (maxLength.HasValue && alreadyRead + readChunkBufferength > maxLength)
                        {
                            readChunkBufferength = maxLength.Value - alreadyRead;
                        }
                    }
                    while (readChunkLength > 0 && readChunkBufferength > 0);

                    if (hasMore.Length > 0)
                    {
                        return textwriter.ToString() + hasMore;
                    }
                    else
                    {
                        try
                        {
                            return System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(textwriter.ToString());
                        }
                        catch
                        {
                            return textwriter.ToString();
                        }
                    }
                }
            }

            stream.Seek(0, SeekOrigin.Begin);
        }
        protected static string Printify(object obj)
        {
            if (obj == null)
                return null;
            if (obj is string)
                return obj.ToString();
            if (obj is JsonDocument)
                obj = ((JsonDocument)obj).RootElement;

            var options = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreNullValues = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            return System.Text.Json.JsonSerializer.Serialize(obj, options);
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

