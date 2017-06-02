using StackExchange.Redis;
using U.BaseService.DistributedCache.Configuration;

namespace U.BaseService.DistributedCache.Redis
{
    /// <summary>
    /// RedisCache管理操作类
    /// </summary>
    public sealed class RedisManager
    {
        /// <summary>
        /// redis配置文件信息
        /// </summary>
        private static RedisConfigInfo _config;

        private static ConnectionMultiplexer _conn;

        /// <summary>
        /// 静态构造方法，初始化链接池管理对象
        /// </summary>
        static RedisManager()
        {
            _config = RedisConfigs.GetConfig();
            CreateManager();
        }


        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private static void CreateManager()
        {

            string[] writeServerList = _config.WriteServerList.Split(",");
            string[] readServerList = _config.ReadServerList.Split(",");
            _conn = ConnectionMultiplexer.Connect(_config.ReadServerList);

            //_prcm = new PooledRedisClientManager(readServerList, writeServerList,
            //                 new RedisClientManagerConfig
            //                 {
            //                     MaxWritePoolSize = _config.MaxWritePoolSize,
            //                     MaxReadPoolSize = _config.MaxReadPoolSize,
            //                     AutoStart = _config.AutoStart
            //                 });

            //var a = 1;
        }

        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        public static IServer GetServer()
        {
            if (_conn == null)
                CreateManager();

            return _conn.GetServer(_config.ReadServerList);
            //return new RedisClient(_config.ReadServerList);
        }

        public static IDatabase GetDatabase() {
            if (_conn == null)
                CreateManager();

            return _conn.GetDatabase();
        }
    }
}
