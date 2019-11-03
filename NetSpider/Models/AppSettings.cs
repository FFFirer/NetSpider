using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NetSpider.Models
{
    public class AppSettings
    {
        public Dictionary<string, string> connectionStrings { get; set; }
        public string this[string name]
        {
            get
            {
                return connectionStrings.GetValueOrDefault(name);
            }
        }
    }

    public class connectionString
    {
        public string name { get; set; }

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
