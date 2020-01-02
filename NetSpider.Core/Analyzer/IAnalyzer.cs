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
        /// 进行分析，返回新产生的任务，抓取到数据由分析器内部处理
        /// </summary>
        /// <param name="task"></param>
        IEnumerable<SpiderTask> Analyze(SpiderTask task);

        void AddDataParser(IDataParser parser);
    }
}
