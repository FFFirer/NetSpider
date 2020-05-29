using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.XieCheng.DB.Entities
{
    /// <summary>
    /// 航线信息
    /// </summary>
    public class Airline
    {
        public long Id { get; set; } 
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
    }
}
