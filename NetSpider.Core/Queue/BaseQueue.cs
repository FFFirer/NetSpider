using System;
using System.Collections.Generic;
using System.Text;
using NetSpider.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace NetSpider.Core
{
    /// <summary>
    /// 基础的架构
    /// </summary>
    public class BaseQueue : IBaseQueue
    {
        #region 任务完成队列/种子待完成任务队列
        private static Queue<SpiderTask> SeedQueue = new Queue<SpiderTask>();
        private static Queue<SpiderTask> AnalysisQueue = new Queue<SpiderTask>();

        /// <summary>
        /// Seed队列获取时的锁
        /// </summary>
        private object SeedLock = new object();

        /// <summary>
        /// Analysis队列获取时的锁
        /// </summary>
        private object AnalysisLock = new object();

        /// <summary>
        /// Seed任务添加事件;
        /// </summary>
        public event EventHandler AddSeedTaskEvent;

        /// <summary>
        /// Analysis任务添加事件
        /// </summary>
        public event EventHandler AddAnalysisTaskEvent;

        /// <summary>
        /// 获取Seed任务
        /// </summary>
        /// <returns></returns>
        public SpiderTask GetSeedTask()
        {
            if(SeedQueue.Count > 0)
            {
                lock (SeedLock)
                {
                    return SeedQueue.Dequeue();
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加Seed任务
        /// </summary>
        /// <param name="task"></param>
        public void AddSeedTask(SpiderTask task)
        {
            SeedQueue.Enqueue(task);
            AddSeedTaskEvent?.Invoke(this, null);
        }

        /// <summary>
        /// 获取Analysis任务
        /// </summary>
        /// <returns></returns>
        public SpiderTask GetAnalysisTask()
        {
            if (AnalysisQueue.Count > 0)
            {
                lock (AnalysisLock)
                {
                    return AnalysisQueue.Dequeue();
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加Analysis任务
        /// </summary>
        /// <param name="task"></param>
        public void AddAnaiysisTask(SpiderTask task)
        {
            AnalysisQueue.Enqueue(task);
            AddAnalysisTaskEvent?.Invoke(this, null);
        }
        #endregion
    }
}
