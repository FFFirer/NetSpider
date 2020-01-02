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

        /// <summary>
        /// 分析
        /// </summary>
        Analysis=4,

        /// <summary>
        /// 存储
        /// </summary>
        Storage=5,

        /// <summary>
        /// 出现程序错误
        /// </summary>
        Exception=6,

        /// <summary>
        /// 终止
        /// </summary>
        Terminated=7
    }

    /// <summary>
    /// 解析的结果类型，0-链接/新的任务，1-内容/需要保存的数据
    /// </summary>
    public enum AnalysisResult
    {
        Init = 0,
        Success = 1,
        Failed = 2,
        Terminated = 3,
        invalid = 4,
    }

    /// <summary>
    /// 存储类型
    /// </summary>
    public enum StorageType
    {
        /// <summary>
        /// Json文本
        /// </summary>
        JsonTxt=0,

        /// <summary>
        /// Mysql数据库
        /// </summary>
        Mysql=1,

        /// <summary>
        /// sql server数据库
        /// </summary>
        SqlServer=2,

        /// <summary>
        /// Mongodb数据库
        /// </summary>
        Mongodb=3
    }
}
