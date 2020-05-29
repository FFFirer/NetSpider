using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.XieCheng.DB.Entities
{
    /// <summary>
    /// 航班信息
    /// </summary>
    public class Flight
    {
        public int Id { get; set; }
        public long TimeTicks { get; set; }
        #region 航班信息
        public string FlightId { get; set; }
        public string FlightNumber { get; set; }
        public string SharedFlightNumber { get; set; }
        public string SharedFlightName { get; set; }
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
        public string CraftTypeCode { get; set; }
        public string CraftKind { get; set; }
        public string CraftTypeName { get; set; }
        public string CraftTypeKindDeisplayName { get; set; }
        #endregion

        #region 出发信息
        public string departureCityTlc { get; set; }
        public string departureCityName { get; set; }
        public string departureAirportTlc { get; set; }
        public string departureAirportName { get; set; }
        public string departureTerminalId { get; set; }
        public string departureTerminalName { get; set; }
        public string departureTerminalShortName { get; set; }
        public string departureDate { get; set; }
        #endregion

        #region 到达信息
        public string arrivalCityTlc { get; set; }
        public string arrivalCityName { get; set; }
        public string arrivalAirportTlc { get; set; }
        public string arrivalAirportName { get; set; }
        public string arrivalTerminalId { get; set; }
        public string arrivalTerminalName { get; set; }
        public string arrivalTerminalShortName { get; set; }
        public string arrivalDate { get; set; }
        #endregion

        #region 其他信息
        public string PunctualityRate { get; set; }
        public string MealType { get; set; }
        public string MealFlag { get; set; }
        public string OilFee { get; set; }
        public string Tax { get; set; }
        public string DurationDays { get; set; }
        public string StopTimes { get; set; }
        public string StopInfo { get; set; }
        #endregion
    }
}
