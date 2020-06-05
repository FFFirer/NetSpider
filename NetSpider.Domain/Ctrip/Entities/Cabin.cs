using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.Domain.Ctrip
{ 
    /// <summary>
    /// 座舱信息
    /// </summary>
    public class Cabin
    {
        public long Id { get; set; }
        public long LinkFlightId { get; set; }
        public string FlightId { get; set; }
        public string CabinId { get; set; }
        public string Pid { get; set; }
        public string SaleType { get; set; }
        public string CabinClass { get; set; }
        public string PriceClass { get; set; }
        public string Price { get; set; }
        public string SalePrice { get; set; }
        public string PrintPrice { get; set; }
        public string FdPrice { get; set; }
        public string Rate { get; set; }
        public string MealType { get; set; }
    }
}
