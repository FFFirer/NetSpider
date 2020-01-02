using NetSpider.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace NetSpider.Core.ConsoleTest.CoreTest
{
    public class LinkParser : BaseDataParser
	{
		public LinkParser(ILogger logger):base(logger)
		{

		}

		public override AnalysisResult Handle(AnalysisContext context)
		{
			try
			{
				string urlpattern = "href=\"(.*?)\"";
				MatchCollection matches = Regex.Matches(context.Content, urlpattern);
				if (matches.Count > 0)
				{
					context.AddTasks(matches.Select(m => m.Groups[1].Value).ToList());
				}
				return AnalysisResult.Success;
			}
			catch (Exception ex)
			{
				_logger.LogDebug(ex, $"{nameof(LinkParser)} error");
				return AnalysisResult.Failed;
			}
		}
    }
}
