using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace NetSpider.Core.Analyzer
{
    /// <summary>
    /// Html页面分析，父类
    /// </summary>
    public abstract class BaseAnalyzer
    {
        private string _name { get; set; }

        /// <summary>
        /// 分析器的名称
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(_name == value)
                {
                    _name = value;
                }
            }
        }

        /// <summary>
        /// 此分析器的页面目标
        /// </summary>
        public SpiderTaskTarget Target { get; set; }

        /// <summary>
        /// 复写此方法，定制从页面中获取的信息。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public abstract T Get<T>(HttpResponseMessage message);
    }
}
