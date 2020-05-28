using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using NetSpider.XieCheng.Models;
using System.Net.Http;
using Microsoft.Extensions.Http;
using System.IO;

namespace NetSpider.XieCheng
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var host = CreateHostBuilder(args).Build())
            {
                host.StartAsync();
                host.WaitForShutdown();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostcontext, services) =>
                {
                    services.AddLogging(logbuilder =>
                    {
                        logbuilder.SetMinimumLevel(LogLevel.Information);
                    });

                    services.AddHttpClient(XieChengProject.ProjectName, c =>
                    {
                        c.BaseAddress = new Uri(XieChengProject.HomeUrl);
                        c.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36");
                        c.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                        c.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
                        c.DefaultRequestHeaders.Add("Accept-Language", "zh,zh-TW;q=0.9,en-US;q=0.8,en;q=0.7,zh-CN;q=0.6");
                    }).ConfigurePrimaryHttpMessageHandler(msgHandler =>
                    {
                        var handler = new HttpClientHandler();
                        if (handler.SupportsAutomaticDecompression)
                        {
                            handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
                        };
                        return handler;
                    }).Services.BuildServiceProvider();

                    var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true).Build();
                    services.Configure<XieChengOptions>(config.GetSection("XieCheng"));
                    services.AddHostedService<XieCheng.Services.XieChengScrapyService>();
                }).UseConsoleLifetime();
        }
    }
}
