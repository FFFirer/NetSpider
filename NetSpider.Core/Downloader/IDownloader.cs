using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NetSpider.Core.Models;

namespace NetSpider.Core.Downloader
{
    public interface IDownloader
    {
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        Task<SpiderTask> RequestAsync(SpiderTask task);
    }
}
