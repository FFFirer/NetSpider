using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.Core.Models
{
    public class AnalysisResult
    {
        public List<SpiderTask> NewTasks { get; set; } = new List<SpiderTask>();
        public List<object> Contents { get; set; } = new List<object>();
    }
}
