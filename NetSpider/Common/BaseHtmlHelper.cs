using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace NetSpider.Common
{
    /// <summary>
    /// 解析Html使用到的基本方法
    /// </summary>
    public class BaseHtmlHelper
    {   
        /// <summary>
        /// Html文档
        /// </summary>
        internal HtmlDocument _doc { get; set; }

        /// <summary>
        /// Html的字符串表示
        /// </summary>
        internal string _htmlcontent { get; set; }

        /// <summary>
        /// 基类的初始化
        /// </summary>
        /// <param name="HtmlContent"></param>
        public BaseHtmlHelper(string HtmlContent)
        {
            _htmlcontent = HtmlContent;
            _doc = new HtmlDocument();
            _doc.LoadHtml(HtmlContent);
        }

        /// <summary>
        /// 转换连续的"\xa0"
        /// </summary>
        /// <param name="content">要转换的内容</param>
        /// <returns>返回转换\xa0之后的内容</returns>
        internal string TransBlank(string content)
        {
            return content?.Replace("&nbsp;&nbsp;&nbsp;", "/")
                ?.Replace("&nbsp;&nbsp;", "/")
                ?.Replace("&nbsp;", "")
                ?.Trim('/');
        }
    }
}
