using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetSpider.Core.Models;
using Microsoft.Extensions.Logging;
using NetSpider.Core.Analyzer;
using System.Linq;

namespace NetSpider.Core
{
    /// <summary>
    /// 分析器调度器
    /// </summary>
    public class AnalysisScheduler
    {
        private readonly ILogger _logger;
        private readonly IAnalyzer _analyzer;

        /// <summary>
        /// 有新任务待处理时触发信号
        /// </summary>
        private AutoResetEvent NewTaskEvent = new AutoResetEvent(false);

        /// <summary>
        /// 产生新种子事件
        /// </summary>
        private event EventHandler<SpiderTask> NewSeedTaskEvent;

        /// <summary>
        /// 保存数据库事件
        /// </summary>
        private event EventHandler<object> SaveDataEvent;

        /// <summary>
        /// 队列
        /// </summary>
        private IBaseQueue _queue;

        public AnalysisScheduler(ILogger<AnalysisScheduler> logger, IAnalyzer analyzer)
        {
            _logger = logger;
            _analyzer = analyzer;
        }

        private bool running { get; set; } = false;
        private CancellationTokenSource _cancelsource { get; set; }
        /// <summary>
        /// 当有新的事件添加的时候，调度器开始处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NewTask(object sender, EventArgs e)
        {
            NewTaskEvent.Set();
        }

        /// <summary>
        /// 和队列互相注册事件
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="token"></param>
        public void Register(IBaseQueue queue, CancellationToken token)
        {
            _cancelsource = CancellationTokenSource.CreateLinkedTokenSource(token);
            _queue = queue;
            // 队列中有新的分析事件
            _queue.AddAnalysisTaskEvent -= NewTask;
            _queue.AddAnalysisTaskEvent += NewTask;
            // 注册此调度器中产生新的种子任务事件是触发
            NewSeedTaskEvent -= AnalysisScheduler_NewSeedTaskEvent;
            NewSeedTaskEvent += AnalysisScheduler_NewSeedTaskEvent;

            _logger.LogDebug($"{nameof(AnalysisScheduler)} Event Registerd!");
        }

        /// <summary>
        /// 当产生新的种子任务时触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnalysisScheduler_NewSeedTaskEvent(object sender, SpiderTask e)
        {
            _queue?.AddSeedTask(e);
        }

        /// <summary>
        /// 处理队列任务
        /// </summary>
        public void Work(CancellationToken token)
        {
            while (running)
            {
                if (token.IsCancellationRequested)
                {
                    throw new OperationCanceledException(token);
                }
                try
                {
                    var task = _queue.GetAnalysisTask();
                    if(task == null)
                    {
                        NewTaskEvent.WaitOne();
                    }
                    else
                    {
                        HandleTask(task);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"解析任务时出现错误");
                }
            }
        }

        /// <summary>
        /// 处理任务 分析页面
        /// </summary>
        /// <param name="task"></param>
        private void HandleTask(SpiderTask task)
        {
            if(task == null)
            {
                _logger.LogError($"{nameof(SpiderTask)} is null");
                return;
            }

            if(_analyzer == null)
            {
                _logger.LogError($"{nameof(_analyzer)} 未实现");
                throw new NullReferenceException($"{nameof(_analyzer)} 未实现");
            }

            List<SpiderTask> NewTasks = _analyzer.Analyze(task).ToList();

            if (NewTasks.Count > 0)
            {
                foreach(var t in NewTasks)
                {
                    NewSeedTaskEvent?.Invoke(null, t);
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
            _logger.LogInformation($"{nameof(AnalysisScheduler)} Started");
        }

        public void Stop()
        {
            _cancelsource.Cancel();
            _logger.LogInformation($"{nameof(AnalysisScheduler)} cancelled");
        }

    }
}
