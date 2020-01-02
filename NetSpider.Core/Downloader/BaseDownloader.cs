using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NetSpider.Core.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace NetSpider.Core.Downloader
{
    /// <summary>
    /// 基础下载器
    /// </summary>
    public abstract class BaseDownloader : IDownloader
    {
        public ILogger _logger;

        public Action<HttpClient> configureHttpClient { get; set; }

        public BaseDownloader(ILoggerFactory loggerFactory)
        {
            if(loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<BaseDownloader>();
        }

        /// <summary>
        /// 需要实现
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract Task<HttpResponseMessage> Request(HttpRequestMessage request);

        /// <summary>
        /// 异步请求任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<SpiderTask> RequestAsync(SpiderTask task)
        {
            HttpResponseMessage response;
            try
            {
                response = await Request(task.Request);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    task.Status = SpiderTaskStatus.Success;
                    task.Response = response;
                }
                else
                {
                    task.Status = SpiderTaskStatus.Fail;
                    task.RetryCount--;
                    task.Response = null;
                }
            }
            catch (Exception ex)
            {
                task.Status = SpiderTaskStatus.Exception;
                task.LastException = ex;
                task.Response = null;
                _logger.LogDebug(ex, $"请求失败！{task.Request.RequestUri.ToString()}");
            }

            return task;
        }
    }
}
