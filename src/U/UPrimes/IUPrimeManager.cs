
namespace U.UPrimes
{
    /// <summary>
    /// 系统的模块管理器接口
    /// </summary>
    public interface IUPrimeManager
    {
        /// <summary>
        /// 初始化所有模块
        /// </summary>
        void InitializeUPrimes();

        /// <summary>
        /// 关闭所有模块
        /// </summary>
        void ShutdownUPrimes();
    }
}
