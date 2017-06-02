using System;
using System.Collections.Generic;
using System.Linq;

namespace U.UPrimes
{
    /// <summary>
    /// 所有使用 U 系统的模块基类，所有的模块定义类必须继承此类
    /// 在应用启用或关闭模块事件中实现一些动作，它还可以定义模块依赖
    /// </summary>
    public abstract class UPrime
    {
        protected internal UPrimeEngine Engine { get; internal set; }

        /// <summary>
        /// 初始化前，这是应用启动调用的第一个事件，这里面的代码，会在依赖注入注册之前运行
        /// </summary>
        public virtual void PreInitialize()
        {

        }

        /// <summary>
        /// 初始化，此方法用于模块的依赖注册
        /// </summary>
        public virtual void Initialize()
        {
            
        }

        /// <summary>
        /// 初始化后，应用启动最后被调用
        /// </summary>
        public virtual void PostInitialize()
        {

        }

        /// <summary>
        /// 应用正在关闭时调用
        /// </summary>
        public virtual void Shutdown()
        {

        }

        /// <summary>
        /// 检查指定的类型是否为UPrime
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsUPrime(Type type)
        {
            return
                type.IsClass &&
                !type.IsAbstract &&
                typeof(UPrime).IsAssignableFrom(type);
        }

        /// <summary>
        /// 查找模块的依赖模块
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public static List<Type> FindDependedUPrimeTypes(Type uprimeType)
        {
            if (!IsUPrime(uprimeType))
            {
                throw new UException("This type is not an UPrime: " + uprimeType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            //如果定义过依赖属性
            if (uprimeType.IsDefined(typeof(DependsOnAttribute), true))
            {
                var dependsOnAttributes = uprimeType.GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedUPrimeTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }

            return list;
        }
    }
}
