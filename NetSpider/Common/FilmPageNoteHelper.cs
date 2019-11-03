using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace NetSpider.Common
{
    /// <summary>
    /// 电影详情页面注释中的内容
    /// </summary>
    public class FilmPageNoteHelper : BaseHtmlHelper
    {
        public FilmPageNoteHelper(string HtmlContent) : base(HtmlContent)
        {

        }

        public HtmlNode GetShootingTimeNode()
        {
            return _doc.DocumentNode.SelectSingleNode("//dt[text()=\"拍摄日期\"]/following-sibling::dd[1]");
        }

        public HtmlNode GetFilmingLocationsNode()
        {
            return _doc.DocumentNode.SelectSingleNode("//dt[text()=\"拍&nbsp;&nbsp;摄&nbsp;&nbsp;地\"]/following-sibling::dd[1]");
        }
    }
}
