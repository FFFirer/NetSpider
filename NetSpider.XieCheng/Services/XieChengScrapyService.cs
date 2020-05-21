using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using NetSpider.XieCheng.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace NetSpider.XieCheng.Services
{
    public class XieChengScrapyService : IHostedService
    {
        private IHttpClientFactory _httpClients;
        private ILogger _logger;

        public XieChengScrapyService(IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
        {
            _httpClients = httpClientFactory;
            _logger = loggerFactory.CreateLogger<XieChengScrapyService>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            GetDataAsync(cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async void GetDataAsync(CancellationToken cancellationToken)
        {
            CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            CancellationToken token = tokenSource.Token;

            HttpClient client = _httpClients.CreateClient(XieChengProject.ProjectName);

            // 访问一下首页
            HttpResponseMessage resp = await client.GetAsync(XieChengProject.HomeUrl);

            

            // 构造请求
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = new Uri(XieChengProject.AirlineApiUrl);
            XieChengProductRequest requestParams = new XieChengProductRequest();
            requestParams.airportParams.Add(new AirportParams()
            {
                acity = "URC",
                acityId = 39,
                acityName = "乌鲁木齐",
                date = "2020-10-01",
                dcity = "SHA",
                dcityId = 2,
                dcityName = "上海",
            });
            requestParams.classType = "ALL";
            requestParams.date = "2020-10-01";
            requestParams.flightWay = "Oneway";
            requestParams.hasBaby = false;
            requestParams.hasChild = false;
            requestParams.searchIndex = 1;
            requestParams.token = "624e8a966a3b5d3298f64dd528ed4ef9";

            var requestContent = new StringContent(JsonConvert.SerializeObject(requestParams));

            

            var responseMessage = await client.PostAsync(XieChengProject.AirlineApiUrl, requestContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                var respData = responseMessage.Content.ReadAsStringAsync().Result;
                object jsonObj = JsonConvert.DeserializeObject(respData);
            }
            else
            {
                _logger.LogError(responseMessage.StatusCode.ToString());
            }
        }
    }
}
