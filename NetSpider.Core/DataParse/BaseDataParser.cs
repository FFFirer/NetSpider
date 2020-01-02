using NetSpider.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace NetSpider.Core
{
    public abstract class BaseDataParser : IDataParser
    {
        public ILogger _logger { get; set; }

        public BaseDataParser(ILogger logger)
        {
            _logger = logger;
        }

        public abstract AnalysisResult Handle(AnalysisContext context);

        public bool CheckContext(AnalysisContext context)
        {
            if(context == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(context.Content))
            {
                return false;
            }

            return true;
        }

        public AnalysisResult Parse(AnalysisContext context)
        {
            if (CheckContext(context))
            {
                this.Handle(context);

                return AnalysisResult.Success;
            }
            else
            {
                return AnalysisResult.invalid;
            }
        }
    }
}
