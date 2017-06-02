using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using StackExchange.Redis;
using U.Utils;
using U.BaseService.DistributedCache.SystemRuntime;
using U.BaseService.DistributedCache.Configuration;

namespace U.BaseService.DistributedCache.Redis
{

    /// <summary>
    /// 使用ServiceStack.Redis方式缓存服务实现
    /// </summary>
    public class RedisCacheManager : IDistributedCacheManager
    {
  //      private readonly IRedisClient _client;
        private readonly IDatabase _database;
        private readonly IServer _server;
        private readonly DistributedCacheConfigInfo _config;
        private readonly RedisConfigInfo _redisConfig;
        public RedisCacheManager()
        {
//            _client = RedisManager.getClient();
            _database = RedisManager.GetDatabase();
            _server = RedisManager.GetServer();
            _config = DistributedCacheConfigs.GetConfig();
            _redisConfig = RedisConfigs.GetConfig();
        }

        #region Keys
        /// <summary>
        /// 从缓存获取当前应用所有的Keys
        /// </summary>
        /// <returns></returns>
        public IList<string> GetAllKeys()
        {
            return GetKeysByPrefix("");
        }

        /// <summary>
        /// 通过前缀获取Keys
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public IList<string> GetKeysByPrefix(string prefix)
        {
            string pattern = GetLocalizedKey(prefix) + "*";

            return Catch<IList<string>>(() =>
            {
                var keys = _server.Keys(0, pattern);
                var list = new List<string>();
                foreach (var k in keys)
                    list.Add(k);

                return list;
            });
        }     
        #endregion


        /// <summary>
        /// 是否存在缓存Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return Catch<bool>(() =>
            {
                //return _client.ContainsKey(GetLocalizedKey(key));
                return _database.KeyExists(GetLocalizedKey(key));
            });

        }

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public void ClearAll()
        {
            Catch<bool>(() =>
            {
                //_client.FlushDb();
                //_server.FlushDatabase();

                var keys = _server.Keys();
                foreach (var key in keys)
                {
                    _database.KeyDelete(key);
                }

                return true;
            });
        }


        /// <summary>
        /// 通过缓存Key从缓存移除数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return Catch<bool>(() =>
            {

                
                //return _client.Remove(GetLocalizedKey(key));

                return _database.KeyDelete(GetLocalizedKey(key));
            });
        }

        /// <summary>
        /// 通过缓存Key前缀从缓存移除数据
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public bool RemoveByPrefix(string prefix) {
            return Catch<bool>(() =>
            {
                string pattern = GetLocalizedKey(prefix) + "*";
                var keys = _server.Keys(0, pattern).ToArray();

                if (keys != null) {
                    //_client.RemoveAll(keys);
                    _database.KeyDelete(keys);
                }
                return true;
            });
        }

        /// <summary>
        /// 通过缓存Key设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, TimeSpan? expireTime = null) where T : class
        {
            return Catch<bool>(() =>
            {
                if (typeof(T) == typeof(string))
                {
                    string strValue = value as string;
                    if (_redisConfig.CompressesFlag)
                    {
                        strValue = CompressHelper.CompressString(strValue);
                    }
                    if (expireTime.HasValue)
                    {
                        //return _client.Set(GetLocalizedKey(key), strValue, expireTime.Value);
                        return _database.StringSet(GetLocalizedKey(key), strValue, expireTime.Value);
                    }
                    else
                    {
                        //return _client.Set(GetLocalizedKey(key), strValue);
                        return _database.StringSet(GetLocalizedKey(key), strValue);
                    }
                }

                string s = null;
                try
                {
                    s = (value == null ? "" : JsonConvert.SerializeObject(value));//约定""为null的情况。
                }
                catch (Exception exp)
                {
                    throw new DistributedCacheSerializationException(typeof(T), exp);
                }

                if (_redisConfig.CompressesFlag)
                {
                    s = CompressHelper.CompressString(s);
                }
                if (expireTime.HasValue)
                {
                    //return _client.Set(GetLocalizedKey(key), Encoding.UTF8.GetBytes(s), expireTime.Value);
                    return _database.StringSet(GetLocalizedKey(key), Encoding.UTF8.GetBytes(s), expireTime.Value);
                    
                }
                else
                {
                    //return _client.Set(GetLocalizedKey(key), Encoding.UTF8.GetBytes(s));
                    return _database.StringSet(GetLocalizedKey(key), Encoding.UTF8.GetBytes(s));
                }
            });
        }

        /// <summary>
        /// 通过缓存Key获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key) where T : class
        {
            return Catch<T>(() =>
            {
                if (typeof(T) == typeof(string))
                {
                    //var strValue = _client.GetValue(GetLocalizedKey(key));
                    var strValue = _database.StringGet(GetLocalizedKey(key)).ToString();
                    if (_redisConfig.CompressesFlag && !string.IsNullOrEmpty(strValue))
                    {
                        strValue = CompressHelper.DecompressString(strValue);
                    }
                    return strValue as T;
                }

                //var bs = _client.GetValue(GetLocalizedKey(key));
                var bs = _database.StringGet(GetLocalizedKey(key)).ToString();
                if (bs == null)
                    return null;
                //string s = Encoding.UTF8.GetString(bs);

                string s = bs;
                try
                {
                    if (_redisConfig.CompressesFlag)
                    {
                        s = CompressHelper.DecompressString(s);
                    }
                    return JsonConvert.DeserializeObject<T>(s);
                }
                catch (Exception exp)
                {
                    throw new DistributedCacheSerializationException(s.NullToEmpty(), exp);
                }
            });
        }

        #region Utilities

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        private T Catch<T>(Func<T> action)
        {
            try
            {
                return action();
            }
            catch (ServiceStack.Redis.RedisException ex)
            {
                throw new DistributedCacheConnectException(ex.Message, ex);
            }
            catch (Newtonsoft.Json.JsonException ex2)
            {
                throw new DistributedCacheSerializationException(ex2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        /// <summary>
        /// 获取缓存键=（config.CacheKeyPrefix + "$" + key）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetLocalizedKey(string key)
        {
            if (!string.IsNullOrEmpty(_config.CacheKeyPrefix))
            {
                key = _config.CacheKeyPrefix + "$" + key;
            }
            else
                key = _config.CacheKeyPrefix + "$";

            return key;
        }
        #endregion

        public void Dispose()
        {
            
        }
    }
}
