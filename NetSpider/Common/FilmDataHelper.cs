using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using HtmlAgilityPack;
using NetSpider.Models;
using System.Text.RegularExpressions;
using System.Linq;

namespace NetSpider.Common
{
    /// <summary>
    /// 页面链接类似https://www.1905.com/mdb/film/2249995/info/
    /// </summary>
    public class FilmDataHelper : BaseHtmlHelper
    {
        public FilmDataHelper(string HtmlContent) : base(HtmlContent){

        }

        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="filmId"></param>
        /// <returns></returns>
        public FilmModel GetFilms(int filmId)
        {
            HtmlNode FilmNameNode = _doc.DocumentNode.SelectSingleNode("//dt[text()=\"片&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名\"]/following-sibling::dd[1]");
            HtmlNode CountryNode = _doc.DocumentNode.SelectSingleNode("//dt[text()=\"国&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;别\"]/following-sibling::dd[1]");
            HtmlNode LanguageNode = _doc.DocumentNode.SelectSingleNode("//dt[text()=\"语&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;言\"]/following-sibling::dd[1]");
            HtmlNode FilmTypeNode = _doc.DocumentNode.SelectSingleNode("//dt[text()=\"影片类型\"]/following-sibling::dd[1]");
            HtmlNode OtherCNFilmNameNode = _doc.DocumentNode.SelectSingleNode("//dt[text()=\"更多中文名\"]/following-sibling::dd[1]/p");
            HtmlNode OtherENFilmNameNode = _doc.DocumentNode.SelectSingleNode("//dt[text()=\"更多外文名\"]/following-sibling::dd[1]/p");
            HtmlNode PlayTimeNode = _doc.DocumentNode.SelectSingleNode("//dt[text()=\"时&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;长\"]/following-sibling::dd[1]");
            HtmlNode ColorNode = _doc.DocumentNode.SelectSingleNode("//dt[text()=\"色&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;彩\"]/following-sibling::dd[1]");
            HtmlNode PlayTypeNode = _doc.DocumentNode.SelectSingleNode("//dt[text()=\"版&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;本\"]/following-sibling::dd[1]");
            HtmlNode PlayInfoNode = _doc.DocumentNode.SelectSingleNode("//dt[text()=\"上映信息\"]/following-sibling::dd[1]/p/text()");
            HtmlNode UserRatingNode = _doc.DocumentNode.SelectSingleNode("//dt[text()=\"用户评分\"]/following-sibling::dd[1]");

            string NotePattern = "<!-- <ul class=\"clearfloat\">(.*?)</ul> -->";
            MatchCollection matches = Regex.Matches(_htmlcontent, NotePattern);

            FilmPageNoteHelper helper = null;
            if (matches.Count > 0)
            {
                helper = new FilmPageNoteHelper(matches.FirstOrDefault().Groups.Values.FirstOrDefault().Value);
            }

            HtmlNode ShootingTimeNode = helper?.GetShootingTimeNode();
            HtmlNode FilmingLocationsNode = helper?.GetFilmingLocationsNode();

            FilmModel film = new FilmModel()
            {
                FilmId = filmId,
                Name = TransBlank(FilmNameNode?.InnerText ?? string.Empty),
                Country = TransBlank(CountryNode?.InnerText ?? string.Empty),
                Language = TransBlank(LanguageNode?.InnerText ?? string.Empty),
                FilmType = TransBlank(FilmTypeNode?.InnerText ?? string.Empty),
                OtherCNFilmName = TransBlank(OtherCNFilmNameNode?.InnerText ?? string.Empty),
                OtherENFilmName = TransBlank(OtherENFilmNameNode?.InnerText ?? string.Empty),
                PlayTime = TransBlank(PlayTimeNode?.InnerText ?? string.Empty),
                Color = TransBlank(ColorNode?.InnerText ?? string.Empty),
                PlayType = TransBlank(PlayTypeNode?.InnerText ?? string.Empty),
                PlayInfo = TransBlank(PlayInfoNode?.InnerText ?? string.Empty),
                ShootingTime = TransBlank(ShootingTimeNode?.InnerText ?? string.Empty),
                FilmingLocations = TransBlank(ShootingTimeNode?.InnerText ?? string.Empty),
                UserRating = TransBlank(UserRatingNode?.InnerText ?? string.Empty)
            };
            return film;
        }
    }
}
