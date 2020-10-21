namespace Tago.Extensions.ExtendedLogging
{
    public class HttpInfo
    {
        public HttpRequestInfo Request { get; set; }
        public HttpResponseInfo Response { get; set; }
    }

    public class HttpContent
    {
        public string Content { get; set; }
    }
}