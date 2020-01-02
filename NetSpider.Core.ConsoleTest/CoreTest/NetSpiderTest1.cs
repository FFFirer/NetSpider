using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NetSpider.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NetSpider.Core.ConsoleTest.CoreTest
{
    public class TestSpiderService1 : IHostedService
    {
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        
        private TestSpider1 testSpider { get; set; }

        public TestSpiderService1(ILoggerFactory factory)
        {
            testSpider = new TestSpider1(factory, _cancellationTokenSource.Token);
            testSpider.AddDataParser(new DataParser1(factory.CreateLogger<DataParser1>()));
            testSpider.AddDataParser(new LinkParser(factory.CreateLogger<LinkParser>()));
            testSpider.AddRequest("https://www.runoob.com/");
            testSpider.ConfigureHttp(client =>
            {
                client.BaseAddress = new Uri("http://www.baidu.com");
            });


        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            testSpider.StartSpider();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            testSpider.StopSpider();
            return Task.CompletedTask;
        }
    }

    public class TestSpider1 : BaseSpider
    {
        public TestSpider1(ILoggerFactory factory, CancellationToken token): base(token, factory)
        {

        }
    }
}
