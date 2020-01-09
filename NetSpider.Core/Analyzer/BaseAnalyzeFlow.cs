using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NetSpider.Core.Models;
using System.Linq;
using Microsoft.Extensions.Logging;
using NetSpider.Core.Storage;

namespace NetSpider.Core.Analyzer
{
    public class BaseAnalyzer : IAnalyzer
    {
        private List<IDataParser> parsers = new List<IDataParser>();

        private IStorageScheduler _storage;

        private ILogger _logger;
        //private IServiceProvider _provider;

        public BaseAnalyzer(IStorageScheduler storageScheduler, ILogger<BaseAnalyzer> logger)
        {
            _storage = storageScheduler;
            _logger = logger;
        }

        public void AddDataParser(IDataParser parser)
        {
            parsers.Add(parser);
        }

        public IEnumerable<SpiderTask> Analyze(SpiderTask task)
        {
            List<SpiderTask> NewTasks = new List<SpiderTask>();
            AnalysisResult result = AnalysisResult.Init;
            AnalysisContext context = new AnalysisContext(task);
            foreach(var parser in parsers)
            {
                result = parser.Parse(context);
                if(result == AnalysisResult.Terminated)
                {
                    break;
                }
            }

            _logger.LogDebug($"ANALYSIS RESULT - Data:{context.HasData}/{context.Datas.Count} Task:{context.HasTasks}/{context.Tasks.Count}");

            if (context.HasData)
            {
                _storage.SaveData(context.Datas);
            }

            if (context.HasTasks)
            {
                NewTasks = context.Tasks.ToList(); ;
            }

            return NewTasks;
        }

    }
}
