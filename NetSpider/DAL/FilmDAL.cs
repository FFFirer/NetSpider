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
    public class FilmDAL : BaseDAL
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
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
        /// <param name="FilmName"></param>
        /// <returns></returns>
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
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddFilmeInfo(FilmModel model)
        {
            string sql = "insert into Film1905 (FilmId, Name, Country, Language, FilmType, OtherCNFilmName, OtherENFilmName, PlayTime, Color, PlayType, PlayInfo, ShootingTime, FilmingLocations, UserRating) " +
                "values(@FilmId, @Name, @Country, @Language, @FilmType, @OtherCNFilmName, @OtherENFilmName, @PlayTime, @Color, @PlayType, @PlayInfo, @ShootingTime, @FilmingLocations, @UserRating)";
            int rows = Execute(sql, model);
            return rows > 0 ? true : false;
        }
    }
}
