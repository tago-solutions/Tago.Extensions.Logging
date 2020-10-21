using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tago.Extensions.Logging;

namespace Tago.Extensions.ExtendedLogging
{    
    public class LogEnrtyEx : LogMessageEntry
    {      

        public LogEnrtyEx() : base() { }

        public LogEnrtyEx(string message) : base(LogLevel.Information, message)
        {

        }

        public LogEnrtyEx(LogLevel level, string message) : base(level, message)
        {

        }



        [JsonConverter(typeof(ExceptionJsonConverter))]
        public override Exception Exception { get; set; }



        public HttpInfo Http { get; set; }

        public void SetHttpRequest(HttpRequestInfo value)
        {
            if (value != null)
            {
                if (this.Http == null)
                    this.Http = new HttpInfo();

                this.Http.Request = value;
            }
        }

        public void SetHttpResponse(HttpResponseInfo value)
        {
            if (value != null)
            {
                if (this.Http == null)
                    this.Http = new HttpInfo();

                this.Http.Response = value;
            }
        }
    }

    public class ExceptionJsonConverter : JsonConverter<Exception>
    {
        public override Exception Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return null;
        }

        public override void Write(Utf8JsonWriter writer, Exception value, JsonSerializerOptions options)
        {
            ExceptionInfo ex = ExceptionInfo.Create(value);
            if(ex == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                JsonSerializer.Serialize<ExceptionInfo>(writer, ex, options);                
            }
            
        }
    }

    //internal static class LogExtensions
    //{
    //    public static void SetMessage(this LogEnrtyEx obj, string value)
    //    {
    //        obj.Extend("message", value);
    //    } 
    //    public static void SetError(this LogEnrtyEx obj, ExceptionInfo value)
    //    {
    //        obj.Extend("error", value);
    //    }

    //    public static void SetHttpRequest(this LogEnrtyEx obj, HttpRequestInfo value)
    //    {
    //        obj.Extend("request", value);
    //    }

    //    public static void SetHttpResponse(this LogEnrtyEx obj, HttpResponseInfo value)
    //    {
    //        obj.Extend("response", value);
    //    }       

    //}
}

