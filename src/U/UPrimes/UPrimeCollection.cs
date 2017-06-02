using System.Collections.Generic;
using System.Linq;

namespace U.UPrimes
{
    /// <summary>
    /// 用于存储YOUModuleInfo的集合
    /// </summary>
    internal class UPrimeCollection : List<UPrimeInfo>
    {
        /// <summary>
        /// 通过类型获取模块实例
        /// </summary>
        /// <typeparam name="TModule">模块类型</typeparam>
        /// <returns></returns>
        public TUPrime GetUPrime<TUPrime>() where TUPrime : UPrime
        {
            var uprime = this.FirstOrDefault(x => x.Type == typeof(TUPrime));
            if (uprime == null)
            {
                throw new UException("Can not find module for " + typeof(TUPrime).FullName);
            }

            return (TUPrime)uprime.Instance;
        }

        /// <summary>
        /// 通用依赖排序后的模块列表（如果A依赖于B，则A会排在B后面）
        /// </summary>
        /// <returns></returns>
        public List<UPrimeInfo> GetSortedUPrimeListByDependency() {
            return this.SortByDependencies(x => x.Dependencies).ToList();
        }
    }
}
