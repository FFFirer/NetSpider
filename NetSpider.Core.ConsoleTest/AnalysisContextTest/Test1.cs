using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NetSpider.Core.Models;
using System.Net.Http;

namespace NetSpider.Core.ConsoleTest
{
    public class Test1 : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Test();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Test()
        {
            SpiderTask task = new SpiderTask("http://www.baidu.com", HttpMethod.Get);
            AnalysisContext context = new AnalysisContext(task);
            Data1 data1 = new Data1
            {
                Name = "test1",
                Age = "age1"
            }; 
            Data1 data2 = new Data1
            {
                Name = "test2",
                Age = "age2"
            };

            Data2 data3 = new Data2
            {
                Title = "title1",
                Content = "content1"
            };
            Data2 data4 = new Data2
            {
                Title = "title2",
                Content = "content2"
            };

            context.AddData(data1);
            context.AddData(data2);
            context.AddData(data3);
            context.AddData(data4);

            int keyCount = context.Datas.Keys.Count;
        }
    }

    public class Data1
    {
        public string Name { get; set; }
        public string Age { get; set; }

    }

    public class Data2
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
