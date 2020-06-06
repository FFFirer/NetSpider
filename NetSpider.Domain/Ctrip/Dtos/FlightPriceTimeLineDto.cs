using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.Domain.Ctrip
{
    public class FlightPriceTimeLineDto
    {
        public string FlightId { get; set; }
        public string FlightNumber { get; set; }
        public string CreateTime { get; set; }
        public string LowestPrice { get; set; }
    }
}
