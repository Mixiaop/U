using U.Settings;

namespace U.Runtime.Caching.Redis.Startup.Configuration
{
    public class RedisCacheSettings : USettings<RedisCacheSettings>
    {
        /// <summary>
        /// Gets connection str key
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets databseid
        /// </summary>
        public int DatabaseId { get; set; }

        /// <summary>
        /// 1=启动 0=未开启
        /// </summary>
        public int Enabled { get; set; }
    }
}
