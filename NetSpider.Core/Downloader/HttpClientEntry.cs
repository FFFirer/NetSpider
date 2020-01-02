using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace NetSpider.Core
{
    /// <summary>
    /// HttpClient入口
    /// </summary>
    public sealed class HttpClientEntry : IDisposable
    {
        public DateTimeOffset LastUseTime { get; set; }
        public HttpClient HttpClient { get; set; }

        public HttpClientEntry(HttpClientHandler handler)
        {
            HttpClient = new HttpClient(handler, true);
        }
        public void Dispose()
        {
            this.HttpClient.Dispose();
        }
    }
}
