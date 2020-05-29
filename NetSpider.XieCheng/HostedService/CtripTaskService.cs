using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetSpider.XieCheng.Services;

namespace NetSpider.XieCheng.HostedService
{
    public class CtripTaskService : IHostedService
    {
        private XieChengScrapyService _ctripService;

        public CtripTaskService(XieChengScrapyService  ctripService)
        {
            _ctripService = ctripService;
        }

        [Obsolete]
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _ctripService.StartAsync(cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _ctripService.StopAsync(cancellationToken);
            return Task.CompletedTask;
        }
    }
}
