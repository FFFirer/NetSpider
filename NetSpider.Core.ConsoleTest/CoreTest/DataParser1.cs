using NetSpider.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace NetSpider.Core.ConsoleTest.CoreTest
{
    public class DataParser1 : BaseDataParser
    {
        public DataParser1(ILogger logger) : base(logger)
        {

        }

        public override AnalysisResult Handle(AnalysisContext context)
        {
            try
            {
                Data1 data = new Data1();

                context.AddData(data);

                return AnalysisResult.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(DataParser1)} error");
                return AnalysisResult.Failed;
            }
        }
    }

    public class Data1
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
