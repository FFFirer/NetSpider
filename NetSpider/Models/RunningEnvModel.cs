using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.Models
{
    public class RunningEnvModel
    {
        public int Id { get; set; }
        public string SpiderKey { get; set; }
        public string CurrentIndex { get; set; } = "A";
        public int CurrentPage { get; set; } = 1;
    }
}
