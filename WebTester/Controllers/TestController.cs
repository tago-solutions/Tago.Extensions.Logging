using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tago.Extensions.ExtendedLogging;

namespace WebTester.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {   
        private readonly IExtendedLogger<TestController, LogEnrtyEx> _logger;

        public TestController(IExtendedLogger<TestController, LogEnrtyEx> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            try
            {
                _logger.Log(new LogEnrtyEx(LogLevel.Warning, "Request")
                {
                    Http = new HttpInfo
                    {
                        Request = await HttpRequestInfo.CreateAsync(HttpContext)
                    }
                });
                
                _logger.Entry.Extend("myMessage", "Hi");
                _logger.Entry.Extend("message", "Hi________________");
                _logger.Entry.Extend("request", await HttpRequestInfo.CreateAsync(HttpContext));
                _logger.FlushEntry();                

                LogEnrtyEx ent = new LogEnrtyEx(LogLevel.Information, "gollaa");                
                ent.Category = "ddddddddd";
                _logger.Log(LogLevel.Information, ent);

                _logger.LogInformation("Test");

                throw new ApplicationException("wow");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
    
}
