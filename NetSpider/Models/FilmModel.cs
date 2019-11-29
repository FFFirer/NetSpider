using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.Models
{
    /// <summary>
    /// 1905网的电影详细资料模型
    /// </summary>
    public class FilmModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 电影ID，1905
        /// </summary>
        public int FilmId { get; set; }
        /// <summary>
        /// 电影名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 影片类型
        /// </summary>
        public string FilmType { get; set; }
        /// <summary>
        /// 更多中文名
        /// </summary>
        public string OtherCNFilmName { get; set; }
        /// <summary>
        /// 更多英文名
        /// </summary>
        public string OtherENFilmName { get; set; }
        /// <summary>
        /// 时长
        /// </summary>
        public string PlayTime { get; set; }
        /// <summary>
        /// 色彩
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string PlayType { get; set; }
        /// <summary>
        /// 放映信息
        /// </summary>
        public string PlayInfo { get; set; }
        /// <summary>
        /// 电影拍摄时间
        /// </summary>
        public string ShootingTime { get; set; }
        /// <summary>
        /// 电影拍摄地点
        /// </summary>
        public string FilmingLocations { get; set; }
        /// <summary>
        /// 用户评分
        /// </summary>
        public string UserRating { get; set; }
    }
}
