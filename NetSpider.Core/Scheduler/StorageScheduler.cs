using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetSpider.Core.Storage
{
    /// <summary>
    /// 持久化调度器，负责将爬虫需要的结果，持久化储存
    /// </summary>
    public class StorageScheduler : IStorageScheduler
    {
        private ILogger _logger;

        private static Queue<Dictionary<string, dynamic>> _storageQueue = new Queue<Dictionary<string, dynamic>>();

        private object _storageLock = new object();

        private AutoResetEvent _newStorageEvent = new AutoResetEvent(false);

        private CancellationTokenSource _source { get; set; }

        public void SaveData(Dictionary<string, dynamic> Datas)
        {
            foreach (string key in Datas.Keys)
            {
                // 查找数据类型是否有对应的存储库
                if(_data2repo.Keys.Count > 0)
                {
                    string reponame = _data2repo[key];

                    if (_repos.ContainsKey(reponame))
                    {
                        _repos[reponame].Save(key, Datas[key]);
                    }
                }
                else
                {
                    _logger.LogWarning($"未设置存储库与数据的映射关系, {nameof(_data2repo)} 为空。");
                }

                //if (Datas[key].Count > 0)
                //{
                //    _logger.LogDebug($"{key} : {Datas[key].Count}");
                //}
            }
        }

        public StorageScheduler(ILogger<StorageScheduler> logger, CancellationToken token)
        {
            _logger = logger;
            _source = CancellationTokenSource.CreateLinkedTokenSource(token);

            Task.Factory.StartNew(() =>
            {
                Work();
            });
        }

        private Dictionary<string, IRepo> _repos { get; set; } = new Dictionary<string, IRepo>();

        public void AddRepo(IRepo repo, string reponame)
        {
            if (!_repos.ContainsKey(reponame))
            {
                _repos.Add(reponame, repo);
            }
        }

        private Dictionary<string, string> _data2repo = new Dictionary<string, string>();

        public void LinkDataAndRepo<T>(string reponame)
        {
            if (!_data2repo.ContainsKey(typeof(T).Name))
            {
                _data2repo.Add(typeof(T).Name, reponame);
            }
        }

        public void Work()
        {
            CancellationToken token = _source.Token;
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    throw new OperationCanceledException(token);
                }

                if (_storageQueue.Count > 0)
                {
                    lock (_storageLock)
                    {
                        var datas = _storageQueue.Dequeue();

                        Save(datas);
                    }
                }
                else
                {
                    _newStorageEvent.WaitOne();
                }
            }
        }

        public void Save(Dictionary<string, dynamic> Datas)
        {
            _storageQueue.Enqueue(Datas);
            _newStorageEvent.Set();
        }
    }
}
