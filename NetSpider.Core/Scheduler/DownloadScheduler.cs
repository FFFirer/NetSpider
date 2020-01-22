using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetSpider.Core.Models;
using Microsoft.Extensions.Logging;
using NetSpider.Core.Downloader;

namespace NetSpider.Core
{
    /// <summary>
    /// 下载调度器
    /// </summary>
    public class DownloadScheduler
    {
        private readonly ILogger _logger;
        private IDownloader _downloader;

        private bool running { get; set; } = false;

        /// <summary>
        /// 有新任务待处理时触发信号
        /// </summary>
        private AutoResetEvent NewTaskEvent = new AutoResetEvent(false);
        
        /// <summary>
        /// 产生新分析任务事件
        /// </summary>
        private event EventHandler<SpiderTask> NewAnalysisTaskEvent;
        
        /// <summary>
        /// 队列
        /// </summary>
        private IBaseQueue _queue;

        private CancellationTokenSource _cancelsource { get; set; }

        /// <summary>
        /// 当有新任务需要被处理的时候，开始处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NewTask(object sender, EventArgs e)
        {
            NewTaskEvent.Set();
        }

        public DownloadScheduler(ILogger<DownloadScheduler> logger, IDownloader downloader)
        {
            _logger = logger;
            _downloader = downloader;
        }

        public void Regiser(IBaseQueue queue, CancellationToken token)
        {
            _cancelsource = CancellationTokenSource.CreateLinkedTokenSource(token);
            _queue = queue;
            // 队列中有新的下载事件
            _queue.AddSeedTaskEvent -= NewTask;
            _queue.AddSeedTaskEvent += NewTask;
            // 注册此调度器中产生新的分析任务时触发事件
            NewAnalysisTaskEvent -= DownloadScheduler_NewAnalysisTaskEvent;
            NewAnalysisTaskEvent += DownloadScheduler_NewAnalysisTaskEvent;
        }

        private void DownloadScheduler_NewAnalysisTaskEvent(object sender, SpiderTask e)
        {
            if (e.Response != null)
            {
                _queue.AddAnaiysisTask(e);
            }
        }

        /// <summary>
        /// 处理队列任务
        /// </summary>
        public async void Work(CancellationToken token)
        {
            while (running)
            {
                if (token.IsCancellationRequested)
                {
                    throw new OperationCanceledException(token);
                }
                try
                {
                    var task = _queue.GetSeedTask();
                    if(task == null)
                    {
                        NewTaskEvent.WaitOne();
                    }
                    else
                    {
                        // 完成请求
                        await _downloader.RequestAsync(task);

                        _logger.LogInformation($"请求完成，{task.Status}，{task.Request.RequestUri}");

                        // 插入新的分析任务
                        NewAnalysisTaskEvent?.Invoke(this, task);

                        // 延迟特定时间
                        Delay();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"处理队列任务时失败");
                }
            }
        }


        public void Start()
        {
            running = true;
            CancellationToken token = _cancelsource.Token;
            Task.Factory.StartNew(() =>
            {
                Work(token);
            });
            _logger.LogInformation($"{typeof(DownloadScheduler).Name} started");
        }

        public void Stop()
        {
            _cancelsource.Cancel();
            _logger.LogInformation($"{typeof(DownloadScheduler).Name} operation cancelled");
        }

        /// <summary>
        /// 延迟固定秒数
        /// </summary>
        public void Delay()
        {
            Thread.Sleep(1000);
        }
    }
}
