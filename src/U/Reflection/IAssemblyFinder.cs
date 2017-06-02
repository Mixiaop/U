using System.Collections.Generic;
using System.Reflection;

namespace U.Reflection
{
    /// <summary>
    /// 程序集查询器接口定义，获取所有的程序集。
    /// 从程序集分析并且找到自己想到的类型
    /// </summary>
    public interface IAssemblyFinder
    {
        /// <summary>
        /// 从应用中获取所有的程序集并返回
        /// </summary>
        /// <returns></returns>
        List<Assembly> GetAllAssemblies();
    }
}
