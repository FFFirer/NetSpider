using System;
using System.Collections.Generic;
using System.Text;

namespace NetSpider.Core.Storage
{
    public interface IStorageScheduler
    {
        /// <summary>
        /// 保存数据组，Key为数据类名，value为数据集合
        /// </summary>
        /// <param name="Datas"></param>
        void Save(Dictionary<string, dynamic> Datas);

        /// <summary>
        /// 添加存储库
        /// </summary>
        /// <param name="repo"></param>
        void AddRepo(IRepo repo, string reponame);

        /// <summary>
        /// 添加数据到存储库的映射关系
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reponame"></param>
        void LinkDataAndRepo<T>(string reponame);
    }
}
