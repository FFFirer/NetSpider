using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NetSpider.Core.Models;
using System.Net.Http;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace NetSpider.Core.ConsoleTest
{
    public class DataParserTestSetvice : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            DataParserTest();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void DataParserTest()
        {
            SpiderTask task = new SpiderTask("http://www.baidu.com", HttpMethod.Get);
            AnalysisContext context = new AnalysisContext(task)
            {
                Content = @"123asd456asd789asd321asd"
            };

            //IDataParser parser = new TestDataParser();
            //AnalysisResult result = parser.Parse(context);

            //int i = 10;
        }

    }

    public class TestDataParser : BaseDataParser
    {
        public TestDataParser(ILogger logger) : base(logger)
        {

        }

        public override AnalysisResult Handle(AnalysisContext context)
        {
            try
            {
                TestData test = new TestData() { Numbers = new List<string>()};
                string p1 = @"([0-9].*?)";
                MatchCollection matches = Regex.Matches(context.Content, p1);
                if (matches.Count > 0)
                {
                    foreach (Match m in matches)
                    {
                        test.Numbers.Add(m.Value);
                    }
                }

                context.AddData(test);
                return AnalysisResult.Success;
            }
            catch (Exception)
            {
                return AnalysisResult.Failed;
            }
        }
    }

    public class TestData
    {
        public List<string> Numbers { get; set; }
    }
}
