using System;
using System.Collections.Generic;
using System.Text;
using NetSpider.Core.Models;

namespace NetSpider.Core.Analyzer
{
    public interface IAnalyzer
    {
        /// <summary>
        /// 进行分析
        /// </summary>
        /// <param name="task"></param>
        AnalysisResult Analyze(SpiderTask task);

        /// <summary>
        /// 抽取目标内容
        /// </summary>
        /// <param name="task"></param>
        void GetContent(SpiderTask task);

        /// <summary>
        /// 抽取目标链接
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        IEnumerable<string> GetLink(SpiderTask task);
    }
}
