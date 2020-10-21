using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tago.Extensions.ExtendedLogging;
using Tago.Extensions.Logging.Formatters;

namespace WebTester
{
    public class Program
    {
        public static void Main(string[] args)
        {            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging(builder =>
                    {
                        builder.ClearProviders();
                        builder.AddLogDependencies(opts =>
                        {
                            opts.SetFormatter<JsonLogFormatter>();                            
                            opts.SetMessageEntryCreatorFactory<LogEnrtyExFactory>();

                        });
                        builder.LogToConsole(cfg =>
                        {
                            //cfg.SetFormatter<MiniConsoleEntryFormatter>();
                            cfg.Configure = cfg =>
                            {
                                cfg.DisableColors = true;                                
                            };                            
                        });

                        builder.AddFile(opts =>
                        {
                            //opts.SetFormatter<MiniFileEntryFormatter>();
                            opts.Configure(cfg =>
                            {
                                cfg.FileName = "ApiTester.netCore3.Log";
                                cfg.LineFormat = "{date: yyyy-MM-dd HH:mm:ss.fff zzz}\t{traceId}\t{machineName}\t{eventId}\t{userName}\t{processId}({threadId})\t[{logLevel}]\t{category}\t{message}";
                            });
                        });

                        //builder.AddDebug();
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
