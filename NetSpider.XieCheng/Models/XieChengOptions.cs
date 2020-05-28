using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace NetSpider.XieCheng.Models
{
    public class XieChengOptions
    {
        public Headers Headers { get; set; }
    }

    public class Headers
    {
        public string UserAgent { get; set; }
        public string Cookie { get; set; }
    }
}
