using System;
using Autofac;

namespace U.Dependency
{
    /// <summary>
    /// 定义用于直接执行依赖注册的任务接口
    /// </summary>
    public interface IIocManager : IIocRegistrar, IIocResolver, IDisposable
    {
        /// <summary>
        /// Ioc容器，所有的依赖注入注册的类型都存在这里
        /// </summary>
        IContainer IocContainer { get; }

        /// <summary>
        /// 检查指定的类型是否注册
        /// </summary>
        /// <param name="type">检查的类型</param>
        /// <returns></returns>
        new bool IsRegistered(Type type);

        /// <summary>
        /// 检查指定的类型是否注册（泛型）
        /// </summary>
        /// <typeparam name="T">检查的类型</typeparam>
        /// <returns></returns>
        new bool IsRegistered<T>();
    }
}
