using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NetSpider.Core.Downloader;
using NetSpider.Core.Common;
using Microsoft.Extensions.Logging;

namespace NetSpider.Core.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world!");
            using (var host = new HostBuilder()
                .ConfigureLogging(logging=> {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureServices((services)=> {
                    services.AddHostedService<CoreSpiderServices>();
                })
                .UseConsoleLifetime()
                .Build())
            {
                host.StartAsync();
                host.WaitForShutdown();
            }
        }
    }
}
