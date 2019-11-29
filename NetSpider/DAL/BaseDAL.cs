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
    /// 操作数据库基础类
    /// </summary>
    public class BaseDAL
    {   
        /// <summary>
        /// 日志组件
        /// </summary>
        ILogger logger = LogManager.GetCurrentClassLogger();
        //public Defaults _defaults { get; set; } = new Defaults();

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectString { get; set; }

        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration _config { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">数据库配置</param>
        /// <param name="connectName">使用的连接字符串名字</param>
        public BaseDAL(IConfiguration configuration, string connectName)
        {
            _config = configuration;
            //_config.GetSection("Defaults").Bind(_defaults);
            ConnectString = configuration.GetConnectionString(connectName);
        }

        /// <summary>
        /// Sql Query，返回null：执行异常
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果，IEnumerable型</returns>
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
        /// <param name="sql">执行语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>执行语句影响的数据行数，-1：出错</returns>
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
