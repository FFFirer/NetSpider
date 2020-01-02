using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace NetSpider.Core.Extensions
{
    public static class NetSpiderExtensions
    {
        public static BaseSpider Configure(this BaseSpider spider, Action<IServiceCollection> configure)
        {
            configure(spider.Services);
            return spider;
        }


    }
}
