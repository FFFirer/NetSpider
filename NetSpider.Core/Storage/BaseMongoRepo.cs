using System;
using System.Collections.Generic;
using System.Text;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace NetSpider.Core.Storage
{
    public class BaseMongoRepo : IRepo
    {
        private string _serverAddress { get; set; }
        private int _port { get; set; }
        private string _userName { get; set; }
        private string _pwd { get; set; }

        private MongoClientSettings _mongosetting { get; set; }
        private MongoCredential _mongocredential { get; set; }

        private MongoClient _client { get; set; }

        #region 构造函数
        private BaseMongoRepo()
        {

        }

        public BaseMongoRepo(string ServerAddress, int Port = 27017)
        {
            if (string.IsNullOrEmpty(ServerAddress))
            {
                throw new ArgumentNullException(nameof(ServerAddress));
            }
            _serverAddress = ServerAddress;
            _port = Port;

            _mongosetting = new MongoClientSettings()
            {
                Server = new MongoServerAddress(_serverAddress, _port)
            };

            _client = new MongoClient(_mongosetting);
        }

        public BaseMongoRepo(string DatabaseName, string UserName, string Pwd, string ServerAddress, int Port = 27017)
        {
            if (string.IsNullOrEmpty(DatabaseName))
            {
                throw new ArgumentNullException(nameof(DatabaseName));
            }

            if (string.IsNullOrEmpty(UserName))
            {
                throw new ArgumentNullException(nameof(UserName));
            }

            if (string.IsNullOrEmpty(Pwd))
            {
                throw new ArgumentNullException(nameof(Pwd));
            }

            if (string.IsNullOrEmpty(ServerAddress))
            {
                throw new ArgumentNullException(nameof(ServerAddress));
            }

            _serverAddress = ServerAddress;
            _port = Port;
            _userName = UserName;
            _pwd = Pwd;

            _mongocredential = MongoCredential.CreateCredential(DatabaseName, _userName, _pwd);
            _mongosetting = new MongoClientSettings()
            {
                Credential = _mongocredential,
                Server = new MongoServerAddress(_serverAddress, _port)
            };
            _client = new MongoClient(_mongosetting);
        }

        #endregion


        public string GetCollectionName(string datatype)
        {
            return "T_" + datatype;
        }

        public void SaveMany<T>(IEnumerable<T> datas)
        {
            string collectionName = GetCollectionName(typeof(T).Name);
            IMongoDatabase database = _client.GetDatabase("netspider");
            var collection = database.GetCollection<T>(collectionName);
            collection.InsertMany(datas);
        }

        public void Save<T>(T data)
        {
            string collectionName = GetCollectionName(typeof(T).Name);
            IMongoDatabase database = _client.GetDatabase("netspider");
            var collection = database.GetCollection<T>(collectionName);
            collection.InsertOne(data);
        }
    }
}
