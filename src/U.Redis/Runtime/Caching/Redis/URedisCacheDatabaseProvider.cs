using System;
using StackExchange.Redis;
using U.Dependency;
using U.Runtime.Caching.Redis.Startup.Configuration;

namespace U.Runtime.Caching.Redis
{
    public class URedisCacheDatabaseProvider : IURedisCacheDatabaseProvider, ISingletonDependency
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;
        private readonly IRedisCacheConfiguration _settings;
        public URedisCacheDatabaseProvider(IRedisCacheConfiguration settings)
        {
            _settings = settings;
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }
        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase(_settings.DatabaseId);
        }

        public ISubscriber GetSubscriber() {
            return _connectionMultiplexer.Value.GetSubscriber();
        }

        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            return ConnectionMultiplexer.Connect(_settings.ConnectionString);
        }
    }
}
