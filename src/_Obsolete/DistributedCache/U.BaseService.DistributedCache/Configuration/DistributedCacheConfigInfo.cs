using System;
using U.Xml.Configuration;

namespace U.BaseService.DistributedCache.Configuration
{
    /// <summary>
    /// 分布式缓存应用配置信息
    /// </summary>
    [Serializable]
    public class DistributedCacheConfigInfo : IConfigInfo
    {
        private bool _apply = true;
        /// <summary>
        /// 是否开启
        /// </summary>
        public bool Apply
        {
            get { return _apply; }
            set { _apply = value; }
        }

        private int _cacheType = 1;
        /// <summary>
        /// 当前使用的缓存类型
        /// </summary>
        public int CacheType {
            get {
                return _cacheType;
            }
            set { _cacheType = value; }
        }


        /// <summary>
        /// 所有缓存KEY使用的前缀
        /// </summary>
        public string CacheKeyPrefix { get; set; }
    }
}
