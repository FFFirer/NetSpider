using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        #region Download
        /// <summary>
        /// 下载调度器
        /// </summary>
        private DownloadScheduler _downloadscheduler = new DownloadScheduler();
        #endregion

        #region Analysis
        /// <summary>
        /// 分析调度器
        /// </summary>
        private AnalysisScheduler _analysisscheduler = new AnalysisScheduler();
        #endregion

        public BaseSpider(CancellationToken token)
        {
            if (_queue != null)
            {
                _downloadscheduler.Regiser(_queue, token);
                _analysisscheduler.Register(_queue, token);
            }
        }
    }
}
