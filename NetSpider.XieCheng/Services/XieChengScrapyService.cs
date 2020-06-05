using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetSpider.XieCheng.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using NetSpider.Domain.Ctrip;

namespace NetSpider.XieCheng.Services
{
    public class XieChengScrapyService
    {
        private IHttpClientFactory _httpClients;
        private ILogger _logger;
        private IServiceCollection _nodeServiceCollections = new ServiceCollection();
        private XieChengOptions _options;
        private TaskOptions _tasks;
        private CtripDbContext _db;
        private HttpClient client;
        private bool running = false;
        private JsManager _jsManager;

        public XieChengScrapyService(IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory, IOptionsMonitor<XieChengOptions> options, IOptionsMonitor<TaskOptions> taskOptions, CtripDbContext ctripDb, IServiceProvider services)
        {
            _httpClients = httpClientFactory;
            _logger = loggerFactory.CreateLogger<XieChengScrapyService>();
            _options = options.CurrentValue;
            _db = ctripDb;
            client = _httpClients.CreateClient(XieChengProject.ProjectName);
            _tasks = taskOptions.CurrentValue;
            _jsManager = services.GetRequiredService<JsManager>();
            _jsManager.LoadScript(Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "demo2.js"));
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew( async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (_tasks.signalflightplans.Count() > 0)
                    {
                        // 判断是否前次任务是否完成
                        if (running)
                        {
                            _logger.LogWarning("last task is running, this time cancelled.");
                        }
                        else
                        {
                            running = true;
                            _logger.LogInformation("start.");
                            foreach (var plan in _tasks.signalflightplans)
                            {
                                // 执行任务
                                await SyncDataAsync(plan, cancellationToken);
                                Thread.Sleep(2000);     // 每次抓取间隔两秒
                            }
                            _logger.LogInformation($"finish. signal flight plan {_tasks.signalflightplans.Count()} done.");
                            running = false;
                        }
                    }
                    else
                    {
                        _logger.LogInformation("no task.");
                    }

                    Thread.Sleep(new TimeSpan(0, _tasks.RunInterval, 0));
                }
            });
        }

        public async Task SyncDataAsync(XieChengProductRequest requestParams, CancellationToken cancellationToken)
        {
            // 构造请求
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = new Uri(XieChengProject.AirlineApiUrl);

            // TODO: 加密的salt改成可配置的
            string input = requestParams.airportParams.FirstOrDefault().dcity + requestParams.airportParams.FirstOrDefault().acity + requestParams.flightWay + "duew&^%5d54nc'KH";
            requestParams.token = _jsManager.Call<string>("m", input);

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
                    cancellationToken.ThrowIfCancellationRequested();
                    foreach (var leg in router["legs"])
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        string flightId = leg["flightId"].ToString();
                        var flightdata = leg["flight"];
                        var cabindata = leg["cabins"];
                        var characteristicdata = leg["characteristic"];

                        // 先获取航班信息
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

                        _db.Flights.Add(flight);
                        _db.SaveChanges();

                        if (flight.Id == 0)
                        {
                            _logger.LogError($"{nameof(flight)}.{nameof(flight.Id)} is 0, save data failed, exit");
                            throw new ArgumentException($"{nameof(flight)}.{nameof(flight.Id)} is 0, save data failed, exit");
                        }

                        // 获取座舱信息
                        foreach (var c in cabindata)
                        {
                            Cabin cabin = new Cabin
                            {
                                FlightId = flightId,
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

                            _db.Cabins.Add(cabin);
                        }

                        // 航班描述
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

                        _db.Characteristics.Add(characteristic);
                        _db.SaveChanges();
                    }
                }
            }
            else
            {
                _logger.LogError(responseMessage.StatusCode.ToString());
            }

        }
    }
}
