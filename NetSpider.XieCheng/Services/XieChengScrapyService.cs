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
using Microsoft.AspNetCore.NodeServices;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.NodeServices.HostingModels;

namespace NetSpider.XieCheng.Services
{
    public class XieChengScrapyService : IHostedService
    {
        private IHttpClientFactory _httpClients;
        private ILogger _logger;
        [Obsolete]
        private readonly INodeServices _nodeServices;
        private IServiceCollection _nodeServiceCollections = new ServiceCollection();
        private XieChengOptions _options;

        [Obsolete]
        public XieChengScrapyService(IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory, IOptionsMonitor<XieChengOptions> options)
        {
            _httpClients = httpClientFactory;
            _logger = loggerFactory.CreateLogger<XieChengScrapyService>();
            _options = options.CurrentValue;
            _nodeServiceCollections.AddNodeServices(options => 
            {
                options.NodeInstanceOutputLogger = loggerFactory.CreateLogger("nodeservices");
                options.ProjectPath = Environment.CurrentDirectory;
            });
            var sp = _nodeServiceCollections.BuildServiceProvider();
            _nodeServices = sp.GetRequiredService<INodeServices>();
        }

        [Obsolete]
        public Task StartAsync(CancellationToken cancellationToken)
        {
            GetDataAsync(cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        [Obsolete]
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
            string input = requestParams.airportParams.FirstOrDefault().dcity + requestParams.airportParams.FirstOrDefault().acity + requestParams.flightWay + "duew&^%5d54nc'KH";
            requestParams.token = await _nodeServices.InvokeAsync<string>("./Scripts/demo", input);

            requestMessage.Headers.Add("Cookie", _options.Headers.Cookie);
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(requestParams));
            requestMessage.Method = HttpMethod.Post;

            var responseMessage = await client.SendAsync(requestMessage);

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
