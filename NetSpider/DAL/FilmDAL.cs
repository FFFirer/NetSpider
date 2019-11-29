using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using NetSpider.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using NLog;

namespace NetSpider.DAL
{   
    /// <summary>
    /// 负责Film的数据库操作
    /// </summary>
    public class FilmDAL : BaseDAL
    {
        /// <summary>
        /// 日志组件
        /// </summary>
        ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 构造函数，使用父类的构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public FilmDAL(IConfiguration configuration):base(configuration, "MsSqlConnection")
        {

        }
        
        /// <summary>
        /// 获取电影数量
        /// </summary>
        public void GetFilmCount()
        {
            string sql = "select count(1) from Film1905";
            var res = Query<int>(sql);
            Console.WriteLine(res.FirstOrDefault()); ;
        }

        /// <summary>
        /// 判断电影是否存在
        /// </summary>
        /// <param name="FilmName">电影名称</param>
        /// <returns>是否存在，true:存在，false:不存在</returns>
        public bool IsExist(int filmid)
        {
            string sql = "select count(1) from Film1905 where FilmId=@FilmId";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("FilmId", filmid);
            var rows = Query<int>(sql, parameters)?.FirstOrDefault();
            return rows > 0 ? true : false;
        }

        /// <summary>
        /// 添加电影信息
        /// </summary>
        /// <param name="model">电影的域模型</param>
        /// <returns>当执行语句影响行数大于0，则表示执行成功，反之就是失败</returns>
        public bool AddFilmeInfo(FilmModel model)
        {
            string sql = "insert into Film1905 (FilmId, Name, Country, Language, FilmType, OtherCNFilmName, OtherENFilmName, PlayTime, Color, PlayType, PlayInfo, ShootingTime, FilmingLocations, UserRating) " +
                "values(@FilmId, @Name, @Country, @Language, @FilmType, @OtherCNFilmName, @OtherENFilmName, @PlayTime, @Color, @PlayType, @PlayInfo, @ShootingTime, @FilmingLocations, @UserRating)";
            int rows = Execute(sql, model);
            return rows > 0 ? true : false;
        }
    }
}
