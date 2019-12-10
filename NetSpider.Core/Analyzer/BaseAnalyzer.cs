using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using NetSpider.Core.Models;
using System.Linq;

namespace NetSpider.Core.Analyzer
{
    /// <summary>
    /// Html页面分析，父类
    /// </summary>
    public abstract class BaseAnalyzer : IAnalyzer
    {
        private string _name { get; set; }
        private readonly List<ILink> LinkParsers;
        private readonly List<IContent> ContentParsers;

        /// <summary>
        /// 分析器的名称
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(_name == value)
                {
                    _name = value;
                }
            }
        }

        /// <summary>
        /// 此分析器的页面目标
        /// </summary>
        public SpiderTaskTarget Target { get; set; }

        /// <summary>
        /// 复写此方法，定制从页面中获取的信息。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public abstract T Get<T>(HttpResponseMessage message);

        public AnalysisResult Analyze(SpiderTask task)
        {
            AnalysisResult result = new AnalysisResult();
            string Content = task.Response.Content.ReadAsStringAsync().Result;
            try
            {
                // 解析链接
                foreach(var parser in LinkParsers)
                {
                    var links = parser.Parse(Content);
                    if(links != null && links.Count() > 0)
                    {
                        foreach(var link in links)
                        {
                            result.NewTasks.Add(new SpiderTask(link, HttpMethod.Get));
                        }
                    }   
                }
                // 解析内容
                foreach(var parser in ContentParsers)
                {
                    parser.Parse(Content);
                    if(parser != null)
                    {
                        result.Contents.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void GetContent(SpiderTask task)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetLink(SpiderTask task)
        {
            throw new NotImplementedException();
        }
    }
}
