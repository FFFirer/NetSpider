using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetSpider.Core.Models;

namespace NetSpider.Core
{
    /// <summary>
    /// 分析器调度器
    /// </summary>
    public class AnalysisScheduler
    {
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

        public AnalysisScheduler()
        {

        }

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
            _queue = queue;
            // 队列中有新的分析事件
            _queue.AddAnalysisTaskEvent -= NewTask;
            _queue.AddAnalysisTaskEvent += NewTask;
            // 注册此调度器中产生新的种子任务事件是触发
            NewSeedTaskEvent -= AnalysisScheduler_NewSeedTaskEvent;
            NewSeedTaskEvent += AnalysisScheduler_NewSeedTaskEvent;

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
        public void Work()
        {

        }
    }
}
