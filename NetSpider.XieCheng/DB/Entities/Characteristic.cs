using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.XieCheng.DB.Entities
{
    /// <summary>
    /// 特征描述
    /// </summary>
    public class Characteristic
    {
        public int Id { get; set; }
        public int LinkedFlightId { get; set; }
        public string FlightId { get; set; }
        public string LowestPrice { get; set; }
        public string LowestPriceId { get; set; }
        public string LowestCfPrice { get; set; }
        public string LowestChildPrice { get; set; }
        public string LowestChildCfPrice { get; set; }
        public string LowestChildAdultPrice { get; set; }
        public string LowestChildAdultCfPrice { get; set; }
    }
}
