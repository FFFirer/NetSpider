using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using NetSpider.Core.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net;
using NetSpider.Core.Common;
using System.Threading;

namespace NetSpider.Core.Downloader
{
    /// <summary>
    /// Downloader默认实现
    /// </summary>
    /// <remarks>
    /// key使用任务名称和代理IP地址的Hash值来区别，区分不同任务之间，同一任务之间不同的代理IP地址
    /// </remarks>
    public class DefaultDownloader : BaseDownloader
    {
        private int _getClientCount;
        /// <summary>
        /// key使用任务名称和代理IP地址的Hash值来区别，区分不同任务之间，同一任务之间不同的代理IP地址。
        /// </summary>
        private readonly ConcurrentDictionary<string, HttpClientEntry> _clients = new ConcurrentDictionary<string, HttpClientEntry>();
        /// <summary>
        /// 是否自动跳转
        /// </summary>
        private bool _allowAutoRedirect { get; set; }
        /// <summary>
        /// 是否使用代理
        /// </summary>
        private bool _useProxy { get; set; }
        /// <summary>
        /// 当前使用的代理
        /// </summary>
        private WebProxy _currentProxy { get; set; }
        /// <summary>
        /// 代理IP池
        /// </summary>
        private IProxyPool _proxyPool { get; set; }
        /// <summary>
        /// 单个任务请求的重试次数
        /// </summary>
        private int _retryTime { get; set; } = 3;
        public DefaultDownloader(ILoggerFactory loggerFactory, IProxyPool proxyPool) : base(loggerFactory)
        {
            if(proxyPool == null)
            {
                //_proxyPool = null;
                //_currentProxy = null;
                _logger.LogError("proxypool is null;");
            }
            else
            {
                _logger.LogError("proxypool isn't null!");
                //_proxyPool = proxyPool;
                //_currentProxy = (WebProxy)_proxyPool.Get();
            }
        }

        /// <summary>
        /// 实现方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async override Task<HttpResponseMessage> Request(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            for(int i=0;i< _retryTime; i++)
            {
                response = null;
                try
                {
                    if (_useProxy)
                    {
                        if (_proxyPool == null)
                        {
                            throw new ArgumentNullException(nameof(_proxyPool));
                        }
                        else
                        {
                            _currentProxy = (WebProxy)_proxyPool.Get();
                            if (_currentProxy == null)
                            {
                                // 无代理IP
                            }
                        }
                    }

                    // 若无法取到代理IP则不使用尝试请求
                    var entry = GetHttpClientEntry(_currentProxy == null ? "Default" : $"{_currentProxy.Address}", _currentProxy);

                    response = await entry.HttpClient.SendAsync(request);

                    _logger.LogInformation($"{DateTime.Now.ToString("o")} => {response.StatusCode}");

                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"{request.RequestUri.ToString()}");
                }
                finally
                {
                    if(_proxyPool!=null && _currentProxy != null)
                    {
                        _proxyPool.CheckEnabled(_currentProxy, response.StatusCode);
                    }
                }

                Thread.Sleep(1000);
            }
            return response;
        }

        /// <summary>
        /// 获取HttpClient的实例
        /// </summary>
        /// <param name="hash">key标识</param>
        /// <param name="proxy">代理</param>
        /// <returns>HttpClientEntry实例</returns>
        /// <remarks>
        /// hash:爬虫名称和代理的hash值，区分不同爬虫，同一爬虫的多个代理。
        /// </remarks>
        public HttpClientEntry GetHttpClientEntry(string hash, IWebProxy proxy)
        {
            if(hash == null)
            {
                throw new ArgumentNullException(nameof(hash));
            }

            Interlocked.Increment(ref _getClientCount);

            if(_getClientCount % 100 == 0)
            {
                CleanPool();
            }

            if (_clients.ContainsKey(hash))
            {
                _clients[hash].LastUseTime = DateTimeOffset.Now;
                return _clients[hash];
            }

            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                UseProxy = _useProxy,
                UseCookies = false,
                AllowAutoRedirect = false,
                Proxy = proxy,
            };

            var entry = new HttpClientEntry(handler);

            configureHttpClient?.Invoke(entry.HttpClient);

            // TODO:这里可以添加默认头信息
            _clients.TryAdd(hash, entry);
            return entry;
        }

        /// <summary>
        /// 清理过期的client
        /// </summary>
        public void CleanPool()
        {
            var needRemoveEntries = new List<string>();
            var now = DateTimeOffset.Now;
            foreach(var pair in _clients)
            {
                if((now-pair.Value.LastUseTime).TotalSeconds > 240)
                {
                    needRemoveEntries.Add(pair.Key);
                }
            }

            foreach(var key in needRemoveEntries)
            {
                var item = _clients[key];
                if(_clients.TryRemove(key, out _))
                {
                    item.Dispose();
                }
            }
        }
    }
}
