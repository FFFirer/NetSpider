using System;
using System.Collections.Generic;
using System.Text;
using NetSpider.Core.Models;

namespace NetSpider.Core
{
    public interface IBaseQueue
    {
        #region 事件
        event EventHandler AddSeedTaskEvent;
        event EventHandler AddAnalysisTaskEvent;
        #endregion

        /// <summary>
        /// 获取Seed任务
        /// </summary>
        /// <returns>种子任务实例</returns>
        SpiderTask GetSeedTask();

        /// <summary>
        /// 添加Seed任务
        /// </summary>
        /// <param name="task">种子任务实例</param>
        void AddSeedTask(SpiderTask task);

        /// <summary>
        /// 获取Analysis任务
        /// </summary>
        /// <returns>分析任务实例</returns>
        SpiderTask GetAnalysisTask();

        /// <summary>
        /// 添加Analysis任务
        /// </summary>
        /// <param name="task">分析任务实例</param>
        void AddAnaiysisTask(SpiderTask task);
    }
}
