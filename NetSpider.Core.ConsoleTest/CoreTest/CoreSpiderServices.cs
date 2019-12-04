using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetSpider.Core.Downloader;
using NetSpider.Core.Models;
using NetSpider.Core.Common;
using System.Net.Http;

namespace NetSpider.Core.ConsoleTest
{
    public class CoreSpiderServices : IHostedService
    {
        private IDownloader _downloader;
        private ILogger _logger;

        //public CoreSpiderServices(
        //    IDownloader downloader,
        //    ILogger<CoreSpiderServices> logger)
        //{
        //    _downloader = downloader;
        //    _logger = logger;
        //}

        public CoreSpiderServices(ILoggerFactory loggerFactory)
        {
            _downloader = new DefaultDownloader(loggerFactory, new DefaultProxyPool());
            _logger = loggerFactory.CreateLogger<CoreSpiderServices>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {

            _logger.LogInformation("Start async");

            await Task.Factory.StartNew(async () =>
            {
                try
                {
                    SpiderTask task = null;
                    for (int i = 0; i < 100; i++)
                    {
                        task = new SpiderTask("http://www.baidu.com", HttpMethod.Get);
                        await _downloader.RequestAsync(task);
                    }
                    _logger.LogInformation(task.Response?.StatusCode.ToString());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, nameof(CoreSpiderServices));
                }
            });
            await Task.Factory.StartNew(async () =>
            {
                try
                {
                    SpiderTask task = null;
                    for (int i = 0; i < 100; i++)
                    {
                        task = new SpiderTask("http://www.baidu.com", HttpMethod.Get);
                        await _downloader.RequestAsync(task);
                    }
                    _logger.LogInformation(task.Response?.StatusCode.ToString());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, nameof(CoreSpiderServices));
                }
            });

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
