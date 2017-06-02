using System;
using StackExchange.Redis;
using U.Json;

namespace U.Runtime.Caching.Redis
{
    /// <summary>
    /// Used to a cache in a Redis server.
    /// </summary>
    public class URedisCache : CacheBase
    {
        private readonly IDatabase _database;
        public URedisCache(string name, IURedisCacheDatabaseProvider redisCacheDatabaseProvider) : base(name) {
            _database = redisCacheDatabaseProvider.GetDatabase();
        }

        public override object GetOrDefault(string key)
        {
            var objbyte = _database.StringGet(GetLocalizedKey(key));
            return objbyte.HasValue
                ? JsonSerializationHelper.DeserializeWithType(objbyte)
                : null;
        }

        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null)
        {
            if (value == null)
            {
                throw new UException("Can not insert null values to the cache!");
            }

            //TODO: This is a workaround for serialization problems of entities.
            //TODO: Normally, entities should not be stored in the cache.
            var type = value.GetType();
            //if (EntityHelper.IsEntity(type) && type.Assembly.FullName.Contains("EntityFrameworkDynamicProxies"))
            //{
            //    type = type.BaseType;
            //}

            _database.StringSet(
                GetLocalizedKey(key),
                JsonSerializationHelper.SerializeWithType(value, type),
                slidingExpireTime
                );
        }

        private string GetLocalizedKey(string key)
        {
            return "n:" + Name + ",k:" + key;
        }

        #region ClearCache
        public void FlushDb()
        {
            _database.ScriptEvaluate("redis.call('flushdb')");
        }

        public void FlushAll() {
            _database.ScriptEvaluate("redis.call('flushall')");
        }

        public override void Remove(string key)
        {
            _database.KeyDelete(GetLocalizedKey(key));
        }

        public override void RemoveByPattern(string pattern)
        {
            _database.KeyDeleteWithPrefix(GetLocalizedKey(pattern + "*"));
        }

        public override void Clear()
        {
            _database.KeyDeleteWithPrefix(GetLocalizedKey("*"));
        }
        #endregion

    }
}
