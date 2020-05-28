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
using Newtonsoft.Json.Linq;
using MySQL.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using NetSpider.XieCheng.DB;
using NetSpider.XieCheng.DB.Entities;

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
        //private CtripDbContext _db;

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
            //_db = ctripDb;
        }

        [Obsolete]
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Factory.StartNew(() =>
            {
                GetDataAsync(cancellationToken);
            }, cancellationToken);
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
            // TODO: 加密的salt改成可配置的
            string input = requestParams.airportParams.FirstOrDefault().dcity + requestParams.airportParams.FirstOrDefault().acity + requestParams.flightWay + "duew&^%5d54nc'KH";
            requestParams.token = await _nodeServices.InvokeAsync<string>("./Scripts/demo", input);

            requestMessage.Headers.Add("Cookie", _options.Headers.Cookie);
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(requestParams));
            requestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            requestMessage.Method = HttpMethod.Post;

            var responseMessage = await client.SendAsync(requestMessage);

            if (responseMessage.IsSuccessStatusCode)
            {
                var respData = responseMessage.Content.ReadAsStringAsync().Result;
                JObject jsonObj = JsonConvert.DeserializeObject<JObject>(respData);
                Type type = jsonObj.GetType();
                var routers = jsonObj["data"]["routeList"];

                long TimeStamp = DateTime.Now.Ticks;

                List<Flight> flights = new List<Flight>();
                List<Cabin> cabins = new List<Cabin>();
                List<Characteristic> characteristics = new List<Characteristic>();

                foreach (var router in routers)
                { 
                    string flightId = router["legs"][0]["flightId"].ToString();
                    var flightdata = router["legs"][0]["flight"];
                    var cabindata = router["legs"][0]["cabins"];
                    var characteristicdata = router["legs"][0]["characteristic"];

                    // 先获取航班
                    Flight flight = new Flight()
                    {
                        TimeTicks = TimeStamp,
                        FlightId = flightId,
                        FlightNumber = flightdata["flightNumber"].ToString(),
                        SharedFlightNumber = flightdata["sharedFlightNumber"].ToString(),
                        SharedFlightName = flightdata["sharedFlightName"]?.ToString() ?? "",
                        AirlineCode = flightdata["airlineCode"].ToString(),
                        AirlineName = flightdata["airlineName"].ToString(),
                        CraftTypeCode = flightdata["craftTypeCode"].ToString(),
                        CraftKind = flightdata["craftKind"].ToString(),
                        CraftTypeName = flightdata["craftTypeName"].ToString(),
                        CraftTypeKindDeisplayName = flightdata["craftTypeKindDisplayName"].ToString(),
                        departureCityTlc = flightdata["departureAirportInfo"]["cityTlc"].ToString(),
                        departureCityName = flightdata["departureAirportInfo"]["cityName"].ToString(),
                        departureAirportTlc = flightdata["departureAirportInfo"]["airportTlc"].ToString(),
                        departureAirportName = flightdata["departureAirportInfo"]["airportName"].ToString(),
                        departureTerminalId = flightdata["departureAirportInfo"]["terminal"]["id"].ToString(),
                        departureTerminalName = flightdata["departureAirportInfo"]["terminal"]["name"].ToString(),
                        departureTerminalShortName = flightdata["departureAirportInfo"]["terminal"]["shortName"].ToString(),
                        departureDate = flightdata["departureDate"].ToString(),
                        arrivalCityTlc = flightdata["arrivalAirportInfo"]["cityTlc"].ToString(),
                        arrivalCityName = flightdata["arrivalAirportInfo"]["cityName"].ToString(),
                        arrivalAirportTlc = flightdata["arrivalAirportInfo"]["airportTlc"].ToString(),
                        arrivalAirportName = flightdata["arrivalAirportInfo"]["airportName"].ToString(),
                        arrivalTerminalId = flightdata["arrivalAirportInfo"]["terminal"]["id"].ToString(),
                        arrivalTerminalName = flightdata["arrivalAirportInfo"]["terminal"]["name"].ToString(),
                        arrivalTerminalShortName = flightdata["arrivalAirportInfo"]["terminal"]["shortName"].ToString(),
                        arrivalDate = flightdata["arrivalDate"].ToString(),
                        PunctualityRate = flightdata["punctualityRate"].ToString(),
                        MealFlag = flightdata["mealFlag"].ToString(),
                        MealType = flightdata["mealType"].ToString(),
                        OilFee = flightdata["oilFee"].ToString(),
                        Tax = flightdata["tax"].ToString(),
                        DurationDays = flightdata["durationDays"].ToString(),
                        StopInfo = flightdata["stopInfo"].ToString(),
                        StopTimes = flightdata["stopTimes"].ToString(),
                    };

                    flights.Add(flight);

                    foreach(var c in cabindata)
                    {
                        Cabin cabin = new Cabin
                        {
                            FlightId = flight.FlightId,
                            LinkFlightId = flight.Id,
                            CabinId = c["id"].ToString(),
                            Pid = c["pid"].ToString(),
                            SaleType = c["saleType"].ToString(),
                            CabinClass = c["cabinClass"].ToString(),
                            PriceClass = c["priceClass"].ToString(),
                            Price = c["price"]["price"].ToString(),
                            SalePrice = c["price"]["salePrice"].ToString(),
                            PrintPrice = c["price"]["printPrice"].ToString(),
                            FdPrice = c["price"]["fdPrice"].ToString(),
                            Rate = c["price"]["rate"].ToString(),
                            MealType = c["mealType"].ToString(),
                        };

                        cabins.Add(cabin);
                    }

                    Characteristic characteristic = new Characteristic
                    {
                        FlightId = flight.FlightId,
                        LinkedFlightId = flight.Id,
                        LowestPrice = characteristicdata["lowestPrice"].ToString(),
                        LowestPriceId = characteristicdata["lowestPriceId"].ToString(),
                        LowestCfPrice = characteristicdata["lowestCfPrice"].ToString(),
                        LowestChildPrice = characteristicdata["lowestChildPrice"].ToString(),
                        LowestChildCfPrice = characteristicdata["lowestChildCfPrice"].ToString(),
                        LowestChildAdultPrice = characteristicdata["lowestChildAdultPrice"].ToString(),
                        LowestChildAdultCfPrice = characteristicdata["lowestChildAdultCfPrice"].ToString(),
                    };

                    characteristics.Add(characteristic);
                }
            }
            else
            {
                _logger.LogError(responseMessage.StatusCode.ToString());
            }

        }
    }
}
