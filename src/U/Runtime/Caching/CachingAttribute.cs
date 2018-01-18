using System;

namespace U.Runtime.Caching
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class CachingAttribute : Attribute
    {
        public CachingAttribute() : this("", CachingBehavior.Get, null)
        {
        }

        public CachingAttribute(string cacheManagerProviderServiceName, CachingBehavior behavior) : this(cacheManagerProviderServiceName, behavior, null)
        {
        }

        public CachingAttribute(string cacheManagerProviderServiceName, CachingBehavior behavior, params string[] methodNames)
        {
            Behavior = behavior;
            CacheManagerProviderServiceName = cacheManagerProviderServiceName;
            MethodNames = methodNames;

        }

        /// <summary>
        /// 同一个项目两种实现ICacheManager的实现，通过别名来区分 （Autofac专用）
        /// 区分如：ZeroBCacheManager、ZeroCacheManager ，缓存存储的位置不一样
        /// </summary>
        public string CacheManagerProviderServiceName { get; set; }

        public CachingBehavior Behavior { get; set; }

        public string[] MethodNames { get; set; }
    }
}
