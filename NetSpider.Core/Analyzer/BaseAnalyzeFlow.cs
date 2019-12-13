using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NetSpider.Core.Models;

namespace NetSpider.Core.Analyzer
{
    public abstract class BaseAnalyzeFlow : IAnalyzer
    {


        public void Analyze(SpiderTask task)
        {

        }

        public abstract void Handle(SpiderTask task);
    }
}
