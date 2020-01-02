using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using NetSpider.Core.Analyzer;
using NetSpider.Core.Downloader;
using NetSpider.Core.Models;
using NetSpider.Core.Common;
using NetSpider.Core.Storage;
using System.Net.Http;

namespace NetSpider.Core
{
    public class BaseSpider
    {
        #region 队列
        /// <summary>
        /// Seed队列和Analysis队列
        /// </summary>
        private IBaseQueue _queue = new BaseQueue();
        #endregion

        private ILoggerFactory _loggerFactory { get; set; }
        private ILogger _logger { get; set; }
        public IServiceCollection Services { get; set; } = new ServiceCollection();
        public IServiceProvider SpiderServiceProvider { get; set; }

        #region Download
        /// <summary>
        /// 下载调度器
        /// </summary>
        private DownloadScheduler _downloadscheduler { get; set; }

        private IDownloader _downloader { get; set; }
        #endregion

        #region Analysis
        /// <summary>
        /// 分析调度器
        /// </summary>
        private AnalysisScheduler _analysisscheduler { get; set; }

        private IAnalyzer _analyzer { get; set; }
        #endregion

        #region Storage
        private IStorageScheduler _storagescheduler { get; set; }
        #endregion

        public BaseSpider(CancellationToken token, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<BaseSpider>();
            Configure();
            Init(token);
        }

        private void Configure()
        {
            SpiderServiceProvider = Services.BuildServiceProvider();

            _analyzer = new BaseAnalyzer(_storagescheduler, _loggerFactory.CreateLogger<BaseAnalyzer>());
            _downloader = new DefaultDownloader(_loggerFactory, new DefaultProxyPool());
        }

        public void Init(CancellationToken token)
        {
            // 获取调度器实例
            _downloadscheduler = new DownloadScheduler(_loggerFactory.CreateLogger<DownloadScheduler>(), _downloader);
            _analysisscheduler = new AnalysisScheduler(_loggerFactory.CreateLogger<AnalysisScheduler>(), _analyzer);
            _storagescheduler = new StorageScheduler(_loggerFactory.CreateLogger<StorageScheduler>(), token);

            // 注册队列到调度器
            if (_queue != null)
            {
                _downloadscheduler.Regiser(_queue, token);
                _analysisscheduler.Register(_queue, token);
            }
        }

        public void AddDataParser(IDataParser parser)
        {
            if (_analyzer != null)
            {
                _analyzer.AddDataParser(parser);
            }
        }

        public void AddRepo(IRepo repo, string reponame)
        {
            if(_storagescheduler != null)
            {
                _storagescheduler.AddRepo(repo, reponame);
            }
        }

        public void AddRequest(string url)
        {
            _queue.AddSeedTask(new SpiderTask(url, HttpMethod.Get));
        }

        public void ConfigureHttp(Action<HttpClient> configureHttpClient)
        {
            _downloader.configureHttpClient = configureHttpClient;
        }

        public void StartSpider()
        {
            _downloadscheduler.Start();
            _analysisscheduler.Start();
        }

        public void StopSpider()
        {
            _downloadscheduler.Stop();
            _analysisscheduler.Stop();
        }
    }
}
