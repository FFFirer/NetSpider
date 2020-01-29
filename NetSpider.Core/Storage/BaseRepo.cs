using System;
using System.Collections.Generic;
using System.Text;
using NetSpider.Core.Models;
using System.Data;

namespace NetSpider.Core.Storage
{
    /// <summary>
    /// 存储基本类
    /// </summary>
    public abstract class BaseRepo : IRepo
    {
        public StorageType storageType { get; set; }

        private Dictionary<string, Queue<dynamic>> _queues = new Dictionary<string, Queue<dynamic>>();

        private Dictionary<string, object> _locks = new Dictionary<string, object>();

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="datatype"></param>
        /// <param name="datas"></param>
        public abstract void Save(string datatype, dynamic datas);

        public void SaveMany<T>(IEnumerable<T> datas)
        {
            throw new NotImplementedException();
        }

        public void Save<T>(T data)
        {
            throw new NotImplementedException();
        }

        public string ConnectionString { get; set; }

        public BaseRepo(IDbConnection connection)
        {
            _connection = connection;
        }

        private IDbConnection _connection { get; set; }
        public IDbConnection Connection
        {
            get
            {
                return _connection;
            }
            set
            {
                if (_connection == value) return;
                _connection = value;
            }
        }


    }
}

