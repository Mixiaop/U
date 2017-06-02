using System;
using System.Collections.Generic;

namespace U.UPrimes
{
    /// <summary>
    /// 从应用中找到所有的模块
    /// </summary>
    public interface IUPrimeFinder
    {
        /// <summary>
        /// 查询所有模块
        /// </summary>
        /// <returns>模块列表</returns>
        ICollection<Type> FindAll();
    }
}
