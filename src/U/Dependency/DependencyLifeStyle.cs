
namespace U.Dependency
{
    /// <summary>
    /// 使用依赖注入的对象的生命周期类型
    /// </summary>
    public enum DependencyLifeStyle
    {
        /// <summary>
        /// 单例（第一次解析时创建对象，之后的解析都会是同一个对象）
        /// </summary>
        Singleton,
        /// <summary>
        /// 瞬时（每一次解析都会创建新的对象）
        /// </summary>
        Transient
    }
}
