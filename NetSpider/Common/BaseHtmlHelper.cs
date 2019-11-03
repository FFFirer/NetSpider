using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace NetSpider.Common
{
    public class BaseHtmlHelper
    {
        internal HtmlDocument _doc { get; set; }
        internal string _htmlcontent { get; set; }

        public BaseHtmlHelper(string HtmlContent)
        {
            _htmlcontent = HtmlContent;
            _doc = new HtmlDocument();
            _doc.LoadHtml(HtmlContent);
        }

        /// <summary>
        /// 转换连续的"\xa0"
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        internal string TransBlank(string content)
        {
            return content?.Replace("&nbsp;&nbsp;&nbsp;", "/")
                ?.Replace("&nbsp;&nbsp;", "/")
                ?.Replace("&nbsp;", "")
                ?.Trim('/');
        }
    }
}
