
namespace U.Runtime.Caching.Redis.Startup.Configuration
{
    public interface IRedisCacheConfiguration
    {
        /// <summary>
        /// Gets connection str key
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Gets databseid
        /// </summary>
        int DatabaseId { get; }

        /// <summary>
        /// 1=启动 0=未开启
        /// </summary>
        int Enabled { get; }
    }
}
