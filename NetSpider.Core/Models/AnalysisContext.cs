using System;
using System.Collections.Generic;
using System.Text;
using NetSpider.Core.Storage;

namespace NetSpider.Core.Models
{
    public class AnalysisContext
    {
        public string Content { get; set; }

        public IServiceProvider Provider { get; set; }

        public SpiderTaskStatus Status { get; set; }
    }
}
