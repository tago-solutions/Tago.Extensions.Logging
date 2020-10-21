using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using System;
using System.Threading.Tasks;

namespace Tago.Extensions.ExtendedLogging
{
    public class HttpRequestInfo : BaseHttpRequestInfo
    {
        private static RecyclableMemoryStreamManager recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        public static async Task<HttpRequestInfo> CreateAsync(HttpContext context)
        {
            HttpRequestInfo res = null;
            try
            {
                context.Request.EnableBuffering();
                await using (var requestStream = recyclableMemoryStreamManager.GetStream())
                {
                    await context.Request.Body.CopyToAsync(requestStream);

                    res = new HttpRequestInfo
                    {
                        //Type = "Request",
                        TraceIdentifier = context.TraceIdentifier,
                        Uri = $"{context.Request.Method} {context.Request.Path.Value}{context.Request.QueryString.Value}",
                        RemoteIpAddress = context.Connection?.RemoteIpAddress?.ToString(),
                        UserId = context.Request.HttpContext.User?.Identity?.Name,
                        Header = HeaderToDictionary(context.Request.Headers),
                        ContentType = context.Request.ContentType,
                        ContentLength = context.Request.ContentLength,
                    };

                    var txt = await ReadStreamInChuncksAsync(requestStream, null);

                    res.Body = new HttpContent
                    {
                        Content = Printify(txt)
                    };
                    //logger.LogInformation(Printify(objToLog));
                    context.Request.Body.Position = 0;

                }
            }
            catch (Exception ex)
            {
                //logger.LogWarning($"could not log http request: {ex.Message}");
            }

            //var res = new HttpRequestInfo
            //{
            //    Scheme = ctx.Request.Scheme,
            //    Uri = ctx.Request.Path,
            //    Header = ctx.Request.Headers.ToDictionary(o => o.Key, b => b.Value.ToString()),
            //    RemoteIpAddress = ctx.Connection.RemoteIpAddress.ToString(),
            //};


            return res;
        }
    }
}