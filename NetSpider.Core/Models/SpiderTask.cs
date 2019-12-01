using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace NetSpider.Core.Models
{
    /// <summary>
    /// 单个url所代表的任务
    /// </summary>
    public class SpiderTask
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
        /// 任务所处的状态
        /// </summary>
        public SpiderTaskStatus Status { get; set; }

        /// <summary>
        /// 剩余的重试次数
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// 该任务的请求
        /// </summary>
        public HttpRequestMessage Request { get; set; }

        /// <summary>
        /// 该任务的响应
        /// </summary>
        public HttpResponseMessage Response { get; set; }


    }

    
}
