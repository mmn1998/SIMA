
//using Newtonsoft.Json;
//using StackExchange.Redis;


//namespace SIMA.Auth.Application.Query.SharedMemory
//{
//    public class RedisCacheProvider : ICacheProvider
//    {

//        ConnectionMultiplexer _connection;
//        ConfigurationOptions _configurationOptions;
//        private IDatabase _db;
//        Settings _configuration;

//        public RedisCacheProvider(Settings configuration)
//        {
//            _configuration = configuration;
//            _configurationOptions = new ConfigurationOptions
//            {
//                EndPoints =
//                {
//                    { _configuration.RedisConf.Host, _configuration.RedisConf.Port }
//                },
//                Ssl = false,
//                Password=_configuration.RedisConf.Password
//            };
//            _connection = ConnectionMultiplexer.Connect(_configurationOptions);

//            //_endPoint = new RedisEndpoint(AppConfig.Instance.redisConf.Host, AppConfig.Instance.redisConf.Port);
//        }
//        //public void Set<T>(string key, T value, int dbId)
//        //{
//        //    this.Set(key, value, TimeSpan.Zero,dbId);
//        //}
//        public void Set<T>(string key, T value, TimeSpan? timeout, int dbId) where T : class
//        {
//            _db = _connection.GetDatabase(dbId);
//            var serializedObject = General.JsonSerialize(value);

//            _db.StringSet(key, serializedObject, timeout);

//        }
//        public long GetCounter(string key, int dbId)
//        {
//            _db = _connection.GetDatabase(dbId);
//            //var val = _db.StringGet(key);
//            //_db.StringIncrement(key)
//            return _db.StringIncrement(key);
//        }
//        public void Reset(string key, int dbId)
//        {
//            _db = _connection.GetDatabase(dbId);
//            _db.StringSet(key, 0);
//        }
//        public bool TryGet<T>(string key, int dbId, out T value) where T : class
//        {
//            _db = _connection.GetDatabase(dbId);
//            var res = _db.StringGet(key);
//            if (res.IsNull)
//            {
//                value = null;
//                return false;

//            }
//            else
//            {
//                value = General.JsonDeserialize<T>(res);
//                return true;
//            }
//        }
//        public bool Remove(string key, int dbId)
//        {
//            _db = _connection.GetDatabase(dbId);
//            return _db.KeyDelete(key);
//        }
//        public bool IsInCache(string key, int dbId)
//        {
//            _db = _connection.GetDatabase(dbId);
//            return _db.KeyExists(key);
//        }
//    }
//}
