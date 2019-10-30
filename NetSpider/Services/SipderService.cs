using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NetSpider.Models;
using System.Net.Http;

namespace NetSpider.Services
{
    /// <summary>
    /// 爬虫主服务
    /// </summary>
    public class SipderService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            
            Console.WriteLine("Start Spider Service");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}
