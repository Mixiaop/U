namespace U.Runtime.Caching
{
    /// <summary>
    /// 定义CachingAttrbute的行为方式
    /// </summary>
    public enum CachingBehavior
    {
        /// <summary>
        /// 从缓存获取数据，如果不存在则从返回值获取并设置到缓存中
        /// </summary>
        Get,
        /// <summary>
        /// 设置缓存
        /// </summary>
        Set,
        /// <summary>
        /// 移除缓存
        /// </summary>
        Remove
    }
}
