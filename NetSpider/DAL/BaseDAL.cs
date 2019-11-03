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
    public class BaseDAL
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        //public Defaults _defaults { get; set; } = new Defaults();

        public string ConnectString { get; set; }

        public IConfiguration _config { get; set; }

        public BaseDAL(IConfiguration configuration, string connectName)
        {
            _config = configuration;
            //_config.GetSection("Defaults").Bind(_defaults);
            ConnectString = configuration.GetConnectionString(connectName);
        }

        /// <summary>
        /// Sql Query，返回null：执行异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object parameters = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(ConnectString))
                {
                    IEnumerable<T> res = connection.Query<T>(sql, parameters);
                    return res;
                    //if (parameters == null)
                    //{
                    //    IEnumerable<T> res = connection.Query<T>(sql);
                    //    return res;
                    //}
                    //else
                    //{

                    //}
                }
            }
            catch (Exception ex)
            {

                logger.Warn(ex);
                return null;
            }
        }

        /// <summary>
        ///  Sql Execute, -1:执行有错误
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int Execute(string sql, object parameters = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(ConnectString))
                {
                    int rows = connection.Execute(sql, parameters);
                    return rows;
                    //if (parameters == null)
                    //{

                    //}
                    //else
                    //{
                    //    int rows = connection.Execute(sql, parameters);
                    //    return rows;
                    //}
                }
            }
            catch (Exception ex)
            {
                logger.Warn(ex);
                return -1;
            }
        }

    }
}
