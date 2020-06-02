using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetSpider.XieCheng.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace NetSpider.XieCheng.HostedService
{
    public class CtripTaskService : BackgroundService
    {
        private XieChengScrapyService _ctripService;
        private ILogger _logger;

        public CtripTaskService(XieChengScrapyService  ctripService, ILogger<CtripTaskService> logger)
        {
            _ctripService = ctripService;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(CtripTaskService)} started. ");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(CtripTaskService)} stoped");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                Task ctriptask = _ctripService.StartAsync(stoppingToken);
                await Task.WhenAll(ctriptask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CtripTaskService)} exited. ");
            }
        }
    }
}
