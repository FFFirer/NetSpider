using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace NetSpider.XieCheng.Models
{
    public class XieChengOptions
    {
        public XieChengOptions()
        {
            Headers = new Headers();
        }

        public Headers Headers { get; set; }
    }

    public class Headers
    {
        public Headers()
        {
            UserAgent = "";
            Cookie = "";
        }

        public string UserAgent { get; set; }
        public string Cookie { get; set; }
    }
}
