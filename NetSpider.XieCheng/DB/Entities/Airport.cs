using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.XieCheng.DB.Entities
{
    /// <summary>
    /// 机场信息
    /// </summary>
    public class Airport
    {
        public long Id { get; set; }
        public string CityTlc { get; set; }
        public string CityName { get; set; }
        public string AirportTlc { get; set; }
        public string AirportName { get; set; }
    }
}
