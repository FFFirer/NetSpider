using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetSpider.Core;
using NetSpider.Core.Models;
using NetSpider.Core.Storage;

namespace NetSpider.Core.ConsoleTest
{
    public class NoverSpiderService : IHostedService
    {
        private CancellationTokenSource _source = new CancellationTokenSource();

        private NoverSpider _spider { get; set; }

        public NoverSpiderService(ILoggerFactory factory)
        {
            _spider = new NoverSpider(factory, _source.Token);

            _spider.AddRequest("http://www.31xs.org/1/1886/");
            _spider.ConfigureHttp(client =>
            {
                client.BaseAddress = new Uri("http://www.31xs.org/1/1886/");
            });
            _spider.AddDataParser(new LinkParser(factory));
            _spider.AddDataParser(new ChapterParser(factory));
            _spider.AddRepo(new XsNoverRepo(), typeof(XsNoverRepo).Name);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _spider.StartSpider();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _spider.StopSpider();
            return Task.CompletedTask;
        }
    }

    public class NoverSpider : BaseSpider
    {
        public NoverSpider(ILoggerFactory factory, CancellationToken token):base(token, factory)
        {

        }
    }

    #region 数据抓取
    public class LinkParser : BaseDataParser
    {
        public LinkParser(ILoggerFactory factory) : base(factory.CreateLogger<LinkParser>())
        {

        }
        public override AnalysisResult Handle(AnalysisContext context)
        {
            return AnalysisResult.Success;
        }
    }

    public class ChapterParser : BaseDataParser
    {
        public ChapterParser(ILoggerFactory factory) : base(factory.CreateLogger<LinkParser>())
        {

        }

        public override AnalysisResult Handle(AnalysisContext context)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region 数据存储
    public class XsNoverRepo : IRepo
    {
        public void Save(string datatype, dynamic datas)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region 数据库模型

    #endregion
}
