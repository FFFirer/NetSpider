using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.Domain.Ctrip
{
    public class FlightSummaryDto
    {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string FlightId { get; set; }
        public string FlightNumber { get; set; }
        public string AirlineName { get; set; }
        public string CraftTypeName { get; set; }
        public string departureCityName { get; set; }
        public string departureAirportName { get; set; }
        public string arrivalCityName { get; set; }
        public string arrivalAirportName { get; set; }
    }
}
