using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using NetSpider.Models;

namespace NetSpider
{
    /// <summary>
    /// 全局配置
    /// </summary>
    public class GlobalConfig
    {
        private GlobalConfig()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();

            appSettings = new AppSettings();
            configuration.GetSection("connectioStrings").Bind(appSettings);
        }

        public static GlobalConfig Instance
        {
            get
            {
                return new GlobalConfig();
            }
        }

        public AppSettings appSettings { get; set; }
    }
}
