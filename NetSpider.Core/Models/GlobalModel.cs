using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.Core
{
    /// <summary>
    /// 任务的抓取任务目标
    /// </summary>
    public enum SpiderTaskTarget
    {
        /// <summary>
        /// 页面内容
        /// </summary>
        Content = 0,

        /// <summary>
        /// 链接
        /// </summary>
        Url = 1
    }

    /// <summary>
    /// 任务的状态
    /// </summary>
    public enum SpiderTaskStatus
    {
        /// <summary>
        /// 等待中
        /// </summary>
        Waiting = 0,

        /// <summary>
        /// 抓取成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 抓取失败
        /// </summary>
        Fail = 2,

        /// <summary>
        /// 重试
        /// </summary>
        Retry = 3,
    }
}
