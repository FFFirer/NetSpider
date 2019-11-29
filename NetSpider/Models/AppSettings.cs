using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NetSpider.Models
{   
    /// <summary>
    /// 系统参数
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 保存所有连接字符串
        /// </summary>
        public Dictionary<string, string> connectionStrings { get; set; }
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="name">连接字符串的标识名</param>
        /// <returns>连接字符串</returns>
        public string this[string name]
        {
            get
            {
                return connectionStrings.GetValueOrDefault(name);
            }
        }
    }

    /// <summary>
    /// 连接字符串
    /// </summary>
    public class connectionString
    {
        /// <summary>
        /// 标识符
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string value { get; set; }
    }


    public class Defaults
    {
        public List<province> provinces { get; set; }
        public Dictionary<string,string> Citys { get; set; }
    }


    public class province
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
