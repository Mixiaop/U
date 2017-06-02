using System.Collections.Generic;

namespace U.Runtime.Caching
{

    /// <summary>
    /// An upper level container for <see cref="ICache"/> objects.
    /// A cache manager should work as Singleton and track and manage <see cref="ICache"/> objects.
    /// ICache 的上级容器，应该使用单例跟踪管理 ICache 对象
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Gets all caches.
        /// 获取所有的缓存
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<ICache> GetAllCaches();

        /// <summary>
        /// Gets (or create) a cache
        /// 通过名称获取（或创建）缓存
        /// Default: name is "U"
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ICache GetCache(string name = "U");
    }
}
