using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetSpider.Domain.Ctrip;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace NetSpider.WebApi.Services
{
    public class CtripServices
    {
        private CtripDbContext _ctripDB;
        private IMapper _mapper;

        public CtripServices(CtripDbContext context, IMapper mapper)
        {
            _ctripDB = context;
            _mapper = mapper;
        }

        /// <summary>
        /// 根据出发日期和起点终点城市名称查询所有飞行计划
        /// </summary>
        /// <param name="departureTime">yyyy-MM-dd</param>
        /// <param name="departureCityName"></param>
        /// <param name="arrarrivalCityName"></param>
        /// <returns></returns>
        public List<FlightSummaryDto> GetAllFlightsByPlan(string departureTime, string departureCityName, string arrarrivalCityName)
        {
            // TimeTicks 是同一次抓取的结果的标识
            List<Flight> flights = _ctripDB.Flights
                .FromSqlRaw<Flight>("select * from flights where TimeTicks=(select max(TimeTicks) from flights where SUBSTRING_INDEX(departureDate, ' ', 1)={0} and departureCityName={1} and arrivalCityName={2})", departureTime, departureCityName, arrarrivalCityName)
                .ToList();
            return _mapper.Map<List<FlightSummaryDto>>(flights);
        }

        public List<FlightPriceTimeLineDto> GetTimeLineDataByFlightNumber(string flightNumber, string departureTime)
        {
            List<FlightPriceTimeLineDto> timeLineData = _ctripDB.Flights
                .FromSqlRaw<Flight>("select * from flights where FlightNumber={0} and SUBSTRING_INDEX(departureDate, ' ', 1)={1} order by TimeTicks desc limit 20", flightNumber, departureTime)
                .Join(_ctripDB.Characteristics, f => f.FlightId, c => c.FlightId, (f, c) => new FlightPriceTimeLineDto()
                {
                    FlightId = f.FlightId,
                    FlightNumber = f.FlightNumber,
                    CreateTime = new DateTime(f.TimeTicks).ToString("yyyy-MM-dd HH:mm:ss"),
                    LowestPrice = c.LowestPrice
                }).ToList();
            return timeLineData;
        }
    }
}
