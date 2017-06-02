using StackExchange.Redis;

namespace U.Runtime.Caching.Redis
{
    /// <summary>
    /// Used to get <see cref="IDatabase"/> for redis cache
    /// </summary>
    public interface IURedisCacheDatabaseProvider
    {
        /// <summary>
        /// Gets the database connnection
        /// </summary>
        /// <returns></returns>
        IDatabase GetDatabase();

        /// <summary>
        /// Gets the subscriber
        /// </summary>
        /// <returns></returns>
        ISubscriber GetSubscriber();
    }
}
