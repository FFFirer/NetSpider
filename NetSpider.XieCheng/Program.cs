using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetSpider.XieCheng.DB;
using NetSpider.XieCheng.Models;
using System;
using System.IO;
using System.Net.Http;
using NLog.Extensions.Logging;
using NLog.Fluent;

namespace NetSpider.XieCheng
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var host = CreateHostBuilder(args).Build())
            {
                host.Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseSystemd()

                .ConfigureServices((hostcontext, services) =>
                {
                    services.AddLogging(logbuilder =>
                    {
                        logbuilder.ClearProviders();
                        logbuilder.SetMinimumLevel(LogLevel.Information);
                        logbuilder.AddNLog("NLog.config");
                    });

                    services.AddHttpClient(XieChengProject.ProjectName, c =>
                    {
                        c.BaseAddress = new Uri(XieChengProject.HomeUrl);
                        c.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36");
                        c.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                        c.DefaultRequestHeaders.Add("Accept", "*/*");
                        c.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
                    }).ConfigurePrimaryHttpMessageHandler(msgHandler =>
                    {
                        var handler = new HttpClientHandler();
                        if (handler.SupportsAutomaticDecompression)
                        {
                            handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
                        };
                        return handler;
                    }).Services.BuildServiceProvider();

                    var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true)
                        .AddJsonFile("tasks.json", optional: true).Build();

                    services.Configure<TaskOptions>(config);
                    services.Configure<XieChengOptions>(config.GetSection("XieCheng"));

                    services.AddSingleton<XieCheng.Services.JsManager>();
                    services.AddScoped<XieCheng.Services.XieChengScrapyService>();
                    services.AddHostedService<XieCheng.HostedService.CtripTaskService>();
                    services.AddDbContext<CtripDbContext>(options => options.UseMySQL(config.GetConnectionString("Mysql")));
                });
        }
    }
}
