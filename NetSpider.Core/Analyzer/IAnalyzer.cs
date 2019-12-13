using System;
using System.Collections.Generic;
using System.Text;
using NetSpider.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace NetSpider.Core.Analyzer
{
    public interface IAnalyzer
    {
        /// <summary>
        /// 进行分析
        /// </summary>
        /// <param name="task"></param>
        void Analyze(SpiderTask task);
    }
}
