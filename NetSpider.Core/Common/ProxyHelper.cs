using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;

namespace NetSpider.Core.Common
{
    /// <summary>
    /// 从代理地址库获取代理地址, 单例模式，全局只保存一个实例
    /// </summary>
    public class ProxyHelper
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static ProxyHelper Instance = new ProxyHelper();

        private static readonly HttpClient _client;

        static ProxyHelper()
        {
            _client = new HttpClient();
        }

        /// <summary>
        /// 获取一个新的代理
        /// </summary>
        /// <returns></returns>
        public IWebProxy GetNewProxy()
        {

        }

        public void FailureProxy(IWebProxy proxy)
        {

        }

        public void FailureProxy(IPAddress address)
        {

        }

    }
}
