using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace U.BaseService.DistributedCache.SystemRuntime
{
    public enum EnumCacheType : int
    {
        /// <summary>
        /// Redis 
        /// 数据存内存,适合内存大小范围内大量缓存。（若是频繁失效的缓存数据，大量热点数据，建议使用redis）
        /// </summary>
        Redis = 1,
        /// <summary>
        /// SSDB
        /// 数据热点存内存，大量数据存磁盘。（若是命中率较低，命中热点数据，大量冷数据，建议使用ssdb）
        /// </summary>
        SSDB = 2,
        /// <summary>
        /// Memcached
        /// </summary>
        Memcached = 3,
        /// <summary>
        /// SQLServer内存表
        /// </summary>
        SqlServer = 4
    }
}
