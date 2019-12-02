using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using NetSpider.Core.Models;

namespace NetSpider.Core.Downloader
{
    public class BaseDownloader
    {
        private IHttpClientFactory _httpClientFactory;

        private string _targetClient;
        private string _useProxy { get; set; }

        public BaseDownloader(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void Work(SpiderTask task)
        {
            HttpClient client = _httpClientFactory.CreateClient(_targetClient);
            
        }
    }
}
