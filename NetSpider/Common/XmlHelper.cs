using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using HtmlAgilityPack;

namespace NetSpider.Common
{
    public class XmlHelper
    {
        public static int GetFilmCount(string content)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);
            HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@class=\"lineG pl10 pb12\"]");
            int Count = 0;
            if (node != null)
            {
                string txt = node.InnerText;
                Count = int.Parse(txt.Replace("共", "").Replace("部影片", ""));              
            }
            return Count;
        }
    }
}
