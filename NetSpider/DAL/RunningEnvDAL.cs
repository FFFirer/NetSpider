using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using NetSpider.Models;
using System.Linq;
using Dapper;

namespace NetSpider.DAL
{
    public class RunningEnvDAL : BaseDAL
    {
        public RunningEnvDAL(IConfiguration configuration):base(configuration, "MsSqlConnection")
        {

        }

        /// <summary>
        /// 根据关键词获取上一次的运行历史
        /// </summary>
        /// <param name="spiderKey">指定抓取任务的关键词</param>
        /// <returns></returns>
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
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveRecord(RunningEnvModel model)
        {
            string sql = model.Id == 0 ? "insert into RunningEnv(SpiderKey, CurrentIndex, CurrentPage)values(@SpiderKey, @CurrentIndex, @CurrentPage)"
                : "update RunningEnv Set CurrentIndex=@CurrentIndex, CurrentPage=@CurrentPage where SpiderKey=@SpiderKey";
            int rows = Execute(sql, model);
            return rows > 0 ? true : false;
        }
    }
}
