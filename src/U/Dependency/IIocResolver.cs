using System;

namespace U.Dependency
{
    /// <summary>
    /// 定义依赖注册的类型解析接口
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// 从容器解析对象并返回
        /// </summary>
        /// <param name="namedAlias"></param>
        /// <returns></returns>
        T Resolve<T>(string namedAlias = "") where T : class;

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="namedAlias"></param>
        /// <returns></returns>
        object Resolve(Type type, string namedAlias = "");

        /// <summary>
        /// 解析所有对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T[] ResolveAll<T>();

        /// <summary>
        /// 解析未注册的对象，也就是帮你创建一个对象（不适用于接口解析）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T ResolveUnregistered<T>() where T : class;
        

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

        /// <summary>
        /// Release pre-resolved object.
        /// 释放预解析的对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        void Release(object obj);
    }
}
