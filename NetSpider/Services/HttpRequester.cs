using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetSpider.Services
{
    /// <summary>
    /// 辅助请求Http请求，测试用，非正式
    /// </summary>
    public class HttpRequester
    {
        /// <summary>
        /// Httpclient共厂
        /// </summary>
        IHttpClientFactory HttpClientFactory;

        /// <summary>
        /// 构造函数，注入HttpClientFactory
        /// </summary>
        /// <param name="httpClientFactory">HttpClientFactory实例</param>
        public HttpRequester(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private HttpRequester() { }

        /// <summary>
        /// 测试获取Html内容
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage GetHtmlContentTest()
        {
            var httpClient = HttpClientFactory.CreateClient("1905");
            var Resp = httpClient.GetAsync("/mdb/film/search/");
            return Resp.Result;
        }
    }
}
