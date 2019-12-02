using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Net.Http;

namespace NetSpider.Core.Downloader
{
    internal class NetHttpClientFactory : IHttpClientFactory, IHttpMessageHandlerFactory
    {
        private readonly ConcurrentDictionary<string, HttpClient>

        public HttpClient CreateClient(string name)
        {
            throw new NotImplementedException();
        }

        public HttpMessageHandler CreateHandler(string name)
        {
            throw new NotImplementedException();
        }
    }
}
