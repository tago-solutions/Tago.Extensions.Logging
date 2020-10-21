using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Tago.Extensions.ExtendedLogging
{
    public class HttpResponseInfo : BaseHttpRequestInfo
    {
        public static async Task<HttpResponseInfo> CreateAsync(HttpContext ctx)
        {
            var res = new HttpResponseInfo
            {
                Scheme = ctx.Request.Scheme,
                Uri = ctx.Request.Path,
                RemoteIpAddress = ctx.Connection.RemoteIpAddress.ToString(),
                Header = ctx.Response.Headers.ToDictionary(o => o.Key, b => b.Value.ToString()),
            };

            var txt = await ReadStreamInChuncksAsync(ctx.Response.Body, null);

            res.Body = new HttpContent
            {
                Content = Printify(txt)
            };
            //logger.LogInformation(Printify(objToLog));
            ctx.Request.Body.Position = 0;


            return res;
        }
    }
}