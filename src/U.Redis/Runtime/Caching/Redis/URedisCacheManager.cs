using StackExchange.Redis;

using U.Startup;

namespace U.Runtime.Caching.Redis
{
    /// <summary>
    /// Used to create <see cref="URedisCache"/> instance.
    /// </summary>
    public class URedisCacheManager : CacheManagerBase
    {
        //private readonly IDatabase _database;
        public URedisCacheManager(IUStartupConfiguration configuration) : base(configuration) {
            //_database = redisCacheDatabaseProvider.GetDatabase();
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return new URedisCache(name, UPrimeEngine.Instance.Resolve<IURedisCacheDatabaseProvider>());
        }

        //public void FlushDb() {
        //    _database.ScriptEvaluate("redis.call('flushdb')");
        //}
    }
}
