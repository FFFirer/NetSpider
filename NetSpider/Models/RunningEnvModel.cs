using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.Models
{
    /// <summary>
    /// 保存抓取电影的当前运行的信息
    /// </summary>
    public class RunningEnvModel
    {
        public int Id { get; set; }
        public string SpiderKey { get; set; }
        public string CurrentIndex { get; set; } = "A";
        public int CurrentPage { get; set; } = 1;
    }
}
