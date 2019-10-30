using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetSpider.Services
{
    public class HttpRequester
    {
        IHttpClientFactory HttpClientFactory;

        public HttpRequester(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        private HttpRequester() { }

        public HttpResponseMessage GetHtmlContentTest()
        {
            var httpClient = HttpClientFactory.CreateClient("1905");
            var Resp = httpClient.GetAsync("/mdb/film/search/");
            return Resp.Result;
        }
    }
}
