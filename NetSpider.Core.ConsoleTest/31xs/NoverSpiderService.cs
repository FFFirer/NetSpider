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
using System.Text.RegularExpressions;
using System.Linq;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using Dapper;

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
                client.BaseAddress = new Uri("http://www.31xs.org");
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
            try
            {
                string chapterLinkPattern = "<dd><a href=\"(.*?)\">(.*?)</a></dd>";

                MatchCollection matches = Regex.Matches(context.Content, chapterLinkPattern, RegexOptions.Multiline);

                if (matches != null && matches.Count > 0)
                {
                    foreach(Match match in matches)
                    {
                        context.AddTask(match.Groups[1].Value.Trim());
                    }
                }

                return AnalysisResult.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Analysis failed, {nameof(LinkParser)}");
                return AnalysisResult.Failed;
            }
        }
    }

    public class ChapterParser : BaseDataParser
    {
        public ChapterParser(ILoggerFactory factory) : base(factory.CreateLogger<LinkParser>())
        {

        }

        public override AnalysisResult Handle(AnalysisContext context)
        {
            try
            {
                // 获取单节小说的章节名称和内容
                string chapNamePattern = "<h1>(.*?)</h1>";
                string contentPattern = "<div id=\"content\">\n(.*?)\n<script>";

                MatchCollection chapnamematches = Regex.Matches(context.Content, chapNamePattern, RegexOptions.Multiline);
                MatchCollection contentmatches = Regex.Matches(context.Content, contentPattern, RegexOptions.Multiline);

                NoverChapter chap = new NoverChapter();

                if (chapnamematches != null && chapnamematches.Count > 0)
                {
                    chap.ChapName = chapnamematches.FirstOrDefault().Groups[1].ToString().Trim();
                }

                if (contentmatches != null && contentmatches.Count > 0)
                {
                    chap.Content = contentmatches.FirstOrDefault().Groups[1].ToString().Trim();
                }

                context.AddData<NoverChapter>(chap);

                return AnalysisResult.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Aanlysis {nameof(NoverChapter)} Failed");
                return AnalysisResult.Failed;
            }
        }
    }
    #endregion

    #region 数据存储
    public class XsNoverRepo : BaseRepo
    {
        public XsNoverRepo():base(new SqlConnection(""))
        {

        }
        public override void Save(string datatype, dynamic datas)
        {
            switch (datatype)
            {
                case "NoverChapter":
                    foreach (var data in datas)
                    {
                        SaveNoverChapter(data);
                    }
                    break;
                default:
                    break;
            }
        }

        private void SaveNoverChapter(NoverChapter chapter)
        {
            string sql = "INSERT INTO Nover1886 (ChapName, Content)VALUES(@ChapName, @Content)";
            Connection.Execute(sql, chapter);
        }
    }
    #endregion

    #region 数据库模型
    public class NoverChapter
    {
        /// <summary>
        /// 章节名称
        /// </summary>
        public string ChapName { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
    #endregion
}
