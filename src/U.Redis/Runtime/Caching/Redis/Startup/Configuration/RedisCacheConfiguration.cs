using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U.Runtime.Caching.Redis.Startup.Configuration
{
    public class RedisCacheConfiguration : IRedisCacheConfiguration
    {
        public RedisCacheConfiguration(RedisCacheSettings settings) {
            ConnectionString = settings.ConnectionString;
            DatabaseId = settings.DatabaseId;
            Enabled = settings.Enabled;
        }

        /// <summary>
        /// Gets connection str key
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Gets databseid
        /// </summary>
        public int DatabaseId { get; private set; }

        public int Enabled { get; private set; }
    }
}
