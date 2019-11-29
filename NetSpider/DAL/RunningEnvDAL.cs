using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using NetSpider.Models;
using System.Linq;
using Dapper;

namespace NetSpider.DAL
{
    /// <summary>
    /// 运行环境相关数据库操作
    /// </summary>
    public class RunningEnvDAL : BaseDAL
    {
        /// <summary>
        /// 构造函数，使用父类构造
        /// </summary>
        /// <param name="configuration"></param>
        public RunningEnvDAL(IConfiguration configuration):base(configuration, "MsSqlConnection")
        {

        }

        /// <summary>
        /// 根据关键词获取上一次的运行历史
        /// </summary>
        /// <param name="spiderKey">指定抓取任务的关键词</param>
        /// <returns>上一次的运行信息</returns>
        public RunningEnvModel GetLast(string spiderKey)
        {
            string sql = "select * from RunningEnv where SpiderKey=@SpiderKey";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("SpiderKey", spiderKey);
            var res = Query<RunningEnvModel>(sql, parameters);
            return res?.FirstOrDefault();
        }

        /// <summary>
        /// 保存记录
        /// </summary>
        /// <param name="model">运行信息</param>
        /// <returns>true：保存成功，false：保存失败</returns>
        public bool SaveRecord(RunningEnvModel model)
        {
            string sql = model.Id == 0 ? "insert into RunningEnv(SpiderKey, CurrentIndex, CurrentPage)values(@SpiderKey, @CurrentIndex, @CurrentPage)"
                : "update RunningEnv Set CurrentIndex=@CurrentIndex, CurrentPage=@CurrentPage where SpiderKey=@SpiderKey";
            int rows = Execute(sql, model);
            return rows > 0 ? true : false;
        }
    }
}
