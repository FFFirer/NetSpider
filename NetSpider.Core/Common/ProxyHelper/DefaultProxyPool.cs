using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;
using NetSpider.Core.Downloader;

namespace NetSpider.Core.Common
{
    /// <summary>
    /// IP代理池默认实现
    /// </summary>
    /// <remarks>
    /// 默认实现依赖于：https://github.com/jhao104/proxy_pool
    /// </remarks>
    public class DefaultProxyPool : IProxyPool
    {
        public DefaultProxyPool()
        {

        }

        public bool CheckEnabled(IWebProxy proxy, HttpStatusCode code)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public IWebProxy Get()
        {
            throw new NotImplementedException();
        }

        public List<IWebProxy> GetSome()
        {
            throw new NotImplementedException();
        }

        public int GetStatus()
        {
            throw new NotImplementedException();
        }
    }
}
