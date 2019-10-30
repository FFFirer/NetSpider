using System;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using NetSpider.Services;
using System.Net.Http;
using System.IO.Compression;
using Microsoft.Extensions.Configuration;

namespace NetSpider
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.SetBasePath(Environment.CurrentDirectory);
                    configApp.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((services) =>
                {
                    services.AddHttpClient("1905", c =>
                    {
                        c.BaseAddress = new Uri("http://www.1905.com");
                        c.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36");
                        c.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                        c.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
                        c.DefaultRequestHeaders.Add("Accept-Language", "zh,zh-TW;q=0.9,en-US;q=0.8,en;q=0.7,zh-CN;q=0.6");
                        c.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    }).ConfigurePrimaryHttpMessageHandler(msgHandler =>
                    {
                        var handler = new HttpClientHandler();
                        if (handler.SupportsAutomaticDecompression)
                        {
                            handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
                        }
                        return handler;
                    }).Services.BuildServiceProvider();

                    services.AddHostedService<Services.SipderService>();
                }).Build();

            host.Start();

            //var serviceProvider = new ServiceCollection()
            //    .AddHttpClient("1905", c =>
            //    {
            //        c.BaseAddress = new Uri("https://www.1905.com");
            //        c.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36");
            //        c.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            //        c.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            //        c.DefaultRequestHeaders.Add("Accept-Language", "zh,zh-TW;q=0.9,en-US;q=0.8,en;q=0.7,zh-CN;q=0.6");
            //        c.DefaultRequestHeaders.Add("Connection", "keep-alive");
            //    }).ConfigurePrimaryHttpMessageHandler( msgHandler => 
            //    {
            //        var handler = new HttpClientHandler();
            //        if (handler.SupportsAutomaticDecompression)
            //        {
            //            handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
            //        }
            //        return handler;
            //    }).Services.BuildServiceProvider();

            //HttpRequester requester = new HttpRequester(serviceProvider.GetService<IHttpClientFactory>());

            //var resp = requester.GetHtmlContentTest();

            //Console.WriteLine(resp.StatusCode);
            //Console.WriteLine(resp.Content.ReadAsStringAsync().Result);

            //Console.ReadKey();
        }
    }
}
