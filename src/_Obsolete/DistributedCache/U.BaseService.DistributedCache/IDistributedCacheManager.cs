using System;
using System.Collections.Generic;
using U.Dependency;

namespace U.BaseService.DistributedCache
{
    /// <summary>
    /// 定义一个缓存基础服务接口，定义数据的基础操作
    /// </summary>
    public interface IDistributedCacheManager : ITransientDependency, IDisposable
    {
        #region Keys
        /// <summary>
        /// 从缓存获取当前应用所有的Keys
        /// </summary>
        /// <returns></returns>
        IList<string> GetAllKeys();

        /// <summary>
        /// 通过前缀获取Keys
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        IList<string> GetKeysByPrefix(string prefix);
        #endregion

        /// <summary>
        /// 是否存在缓存Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// 通过缓存Key从缓存移除数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);

        /// <summary>
        /// 通过缓存Key前缀从缓存移除数据
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        bool RemoveByPrefix(string prefix);

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        void ClearAll();

        /// <summary>
        /// 通过缓存Key设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        bool Set<T>(string key, T value, TimeSpan? expireTime = null) where T : class;

        /// <summary>
        /// 通过缓存Key获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key) where T : class;

    }
}
