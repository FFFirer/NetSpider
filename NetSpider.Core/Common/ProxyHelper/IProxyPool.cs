using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace NetSpider.Core.Common
{
    /// <summary>
    /// 代理IP池接口
    /// </summary>
    public interface IProxyPool
    {
        /// <summary>
        /// 获取一个代理IP
        /// </summary>
        /// <returns></returns>
        IWebProxy Get();

        /// <summary>
        /// 获取几个接口
        /// </summary>
        /// <returns></returns>
        List<IWebProxy> GetSome();

        /// <summary>
        /// 获取代理数量
        /// </summary>
        /// <returns></returns>
        int GetStatus();

        /// <summary>
        /// 删除一个代理
        /// </summary>
        void Delete();

        /// <summary>
        /// 判断代理是否可用
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        bool CheckEnabled(IWebProxy proxy, HttpStatusCode code);
    }
}
