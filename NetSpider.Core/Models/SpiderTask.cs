using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace NetSpider.Core.Models
{
    /// <summary>
    /// 单个url所代表的任务
    /// </summary>
    public sealed class SpiderTask:IDisposable
    {
        /// <summary>
        /// 抓取目标，链接/内容
        /// </summary>
        public SpiderTaskTarget Target { get; set; }

        /// <summary>
        /// 要抓取的链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 任务所处的状态，按需设置
        /// </summary>
        public SpiderTaskStatus Status { get; set; } = SpiderTaskStatus.Waiting;

        /// <summary>
        /// 重新进入任务队列的次数
        /// </summary>
        public int RetryCount { get; set; } = 3;

        /// <summary>
        /// 该任务的请求
        /// </summary>
        public HttpRequestMessage Request { get; set; }

        /// <summary>
        /// 该任务的响应
        /// </summary>
        public HttpResponseMessage Response { get; set; }

        public Exception LastException { get; set; }

        public SpiderTask(string url, HttpMethod method)
        {
            Url = url;
            Request = new HttpRequestMessage(method, url);
        }

        public void Dispose()
        {
            Request?.Dispose();
            Response?.Dispose();
        }
    }
}
