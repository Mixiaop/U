using System;
using System.Collections.Generic;
using System.Reflection;

namespace U.Reflection
{
    /// <summary>
    /// 类型查询接口定义
    /// 主要用于从Assemble里找到想到的类型
    /// </summary>
    public interface ITypeFinder
    {
        /// <summary>
        /// 通过委托查询类型集合
        /// </summary>
        /// <param name="predicate">委托表达式</param>
        /// <returns></returns>
        Type[] Find(Func<Type, bool> predicate);

        /// <summary>
        /// 查询所有的类型集合
        /// </summary>
        /// <returns></returns>
        Type[] FindAll();

        IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    }
}
