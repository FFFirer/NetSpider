using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.XieCheng.Models
{
    public class XieChengProductRequest
    {
        public XieChengProductRequest()
        {
            airportParams = new List<AirportParams>();
        }
        public List<AirportParams> airportParams { get; set; }
        public string classType { get; set; }
        public string date { get; set; }
        public string flightWay { get; set; }
        public bool hasBaby { get; set; }
        public bool hasChild { get; set; }
        public int searchIndex { get; set; }
        public string token { get; set; }
    }

    public class AirportParams
    {
        public string acity { get; set; }
        public int acityId { get; set; }
        public string acityName { get; set; }
        public string date { get; set; }
        public string dcity { get; set; }
        public int dcityId { get; set; }
        public string dcityName { get; set; }
    }
}
