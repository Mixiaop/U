using System;
using System.Reflection;

namespace U.Dependency
{
    /// <summary>
    /// 定义依赖注入的注册类接口
    /// </summary>
    public interface IIocRegistrar
    {
        /// <summary>
        /// 为自定义的注册器添加依赖注册
        /// </summary>
        /// <param name="registrar">注册器</param>
        void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar);

        /// <summary>
        /// 为给矛的程序集注册所有的依赖，通过注册的依赖注册器（AddConventionalRegistrar）
        /// 如 .AddConventionalRegistrar() 添加的
        /// 默认有 BasicConventionalRegistrar
        /// </summary>
        /// <param name="assembly">Assembly to register</param>
        void RegisterAssemblyByConvention(Assembly assembly);

        /// <summary>
        /// 注册一个类型（泛型）
        /// </summary>
        /// <typeparam name="T">注册的类型</typeparam>
        /// <param name="lifeStyle">对象的生命周期</param>
        /// <param name="name"></param>
        void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton, string namedAlias = "") where T : class;

        /// <summary>
        /// 注册一个类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="lifeStyle">对象的生命周期</param>
        /// <param name="name"></param>
        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton, string namedAlias = "");

        /// <summary>
        /// 注册一个类型和它的实现类型
        /// </summary>
        /// <typeparam name="TType">注册类型</typeparam>
        /// <typeparam name="TImpl">实现类型</typeparam>
        /// <param name="lifeStyle">对象的生命周期</param>
        /// <param name="namedAlias"></param>
        void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton, string namedAlias = "");

        /// <summary>
        /// 注册一个类型和它的实现类型（泛型）
        /// </summary>
        /// <param name="type">注册类型</param>
        /// <param name="impl">实现类型</param>
        /// <param name="lifeStyle">对象的生命周期</param>
        /// <param name="namedAlias">别名</param>
        void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton, string namedAlias = "");

        /// <summary>
        /// 检查指定的类型是否注册（泛型）
        /// </summary>
        /// <typeparam name="T">检查的类型</typeparam>
        /// <returns></returns>
        bool IsRegistered<T>(string namedAlias = "");

        /// <summary>
        /// 检查指定的类型是否注册
        /// </summary>
        /// <param name="type">检查的类型</param>
        /// <param name="namedAlias"></param>
        /// <returns></returns>
        bool IsRegistered(Type type, string namedAlias = "");
    }
}
