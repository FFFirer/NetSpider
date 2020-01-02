using System;
using System.Collections.Generic;
using System.Text;
using NetSpider.Core.Models;

namespace NetSpider.Core
{
    public interface IDataParser
    {
        /// <summary>
        /// 解析一个内容的字符串并返回一个结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origin"></param>
        /// <returns></returns>
        public AnalysisResult Parse(AnalysisContext context);
    }
}
