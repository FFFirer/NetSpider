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

        /// <summary>
        /// 指定编码格式，优先级最高
        /// </summary>
        public string SpecifyEncoding { get; set; }

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
        /// <summary>
        /// 将响应转换为正确编码的字符串
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string GetContent()
        {
            if (Response == null)
            {
                throw new ArgumentNullException(nameof(Response));
            }

            if (Response.Content == null)
            {
                throw new NullReferenceException(nameof(Response.Content));
            }

            if (!string.IsNullOrEmpty(SpecifyEncoding))
            {
                return Encoding.GetEncoding(SpecifyEncoding).GetString(Response.Content.ReadAsByteArrayAsync().Result);
            }

            if (Response?.Content.Headers.ContentType?.CharSet != null)
            {
                // 根据编码类型解析
                return Encoding.GetEncoding(Response.Content.Headers.ContentType.CharSet).GetString(Response.Content.ReadAsByteArrayAsync().Result);
            }
            else
            {
                // 返回默认字符串
                return Response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
