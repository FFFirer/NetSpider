using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.XieCheng.Models
{
    public class TaskOptions
    {
        public int RunInterval { get; set; }
        /// <summary>
        /// 具体单个航班
        /// </summary>
        public List<XieChengProductRequest> signalflightplans { get; set; }

        /// <summary>
        /// 范围时间内的抓取计划
        /// </summary>
        public List<RangeFlightPlan> rangeflightplans { get; set; }
    }

    /// <summary>
    /// 一段时间内每天的航班信息
    /// </summary>
    public class RangeFlightPlan
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public XieChengProductRequest flightplan { get; set; }
    }
}
