using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Linq;

namespace NetSpider.Common
{
    /// <summary>
    /// 链接类似https://www.1905.com/mdb/film/list/enindex-A/
    /// </summary>
    public class FilmListPageHelper : BaseHtmlHelper
    {
        public FilmListPageHelper(string HtmlContent) : base(HtmlContent)
        {

        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public int GetFilmCount()
        {
            HtmlNode node = _doc.DocumentNode.SelectSingleNode("//div[@class=\"lineG pl10 pb12\"]");
            int Count = 0;
            if (node != null)
            {
                string txt = node.InnerText;
                Count = int.Parse(txt.Replace("共", "").Replace("部影片", ""));
            }
            return Count;
        }

        /// <summary>
        /// 获取列表页面上所有影片的链接和Id
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetFilmsUrls()
        {
            HtmlNodeCollection FilmNodeCollection = _doc.DocumentNode.SelectNodes("/html/body/div[2]/div[1]/ul/li/a");
            Dictionary<string, string> films = new Dictionary<string, string>();
            foreach (HtmlNode node in FilmNodeCollection)
            {
                string url = node.GetAttributeValue("href", "");
                string Id = url.Trim('/').Split('/').LastOrDefault();
                if (!string.IsNullOrEmpty(Id))
                {
                    films.Add(Id, url);
                }
            }

            return films;
        }
    }
}
