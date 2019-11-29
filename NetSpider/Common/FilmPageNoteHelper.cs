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
        /// <summary>
        /// 构造函数，使用父类的构造函数
        /// </summary>
        /// <param name="HtmlContent"></param>
        public FilmPageNoteHelper(string HtmlContent) : base(HtmlContent)
        {

        }

        /// <summary>
        /// 获取拍摄日期的html节点
        /// </summary>
        /// <returns>拍摄日期的html节点</returns>
        public HtmlNode GetShootingTimeNode()
        {
            return _doc.DocumentNode.SelectSingleNode("//dt[text()=\"拍摄日期\"]/following-sibling::dd[1]");
        }

        /// <summary>
        /// 获取拍摄地点的Html节点
        /// </summary>
        /// <returns>拍摄地点的Html节点</returns>
        public HtmlNode GetFilmingLocationsNode()
        {
            return _doc.DocumentNode.SelectSingleNode("//dt[text()=\"拍&nbsp;&nbsp;摄&nbsp;&nbsp;地\"]/following-sibling::dd[1]");
        }
    }
}
