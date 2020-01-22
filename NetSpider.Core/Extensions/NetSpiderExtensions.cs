using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NetSpider.Core.Storage;

namespace NetSpider.Core.Extensions
{
    public static class NetSpiderExtensions
    {
        public static BaseSpider Configure(this BaseSpider spider, Action<IServiceCollection> configure)
        {
            configure(spider.Services);
            return spider;
        }

        public static BaseSpider UseMongoDB(this BaseSpider spider, Func<IRepo> CreateMongo)
        {
            IRepo repo = CreateMongo();
            spider.AddRepo(repo, repo.GetType().FullName);
            return spider;
        }
    }
}
