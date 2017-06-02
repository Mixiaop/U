using System;
using System.Collections.Generic;
using System.Reflection;

namespace U.UPrimes
{
    /// <summary>
    /// 用于存储一个模块的信息类，在模块管理器中代表了一个YOUModule
    /// </summary>
    internal class UPrimeInfo
    {
        /// <summary>
        /// 模块程序集
        /// </summary>
        public Assembly Assembly { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 模块的实例
        /// </summary>
        public UPrime Instance { get; set; }

        /// <summary>
        /// 此模块依赖的所有模块
        /// </summary>
        public List<UPrimeInfo> Dependencies { get; private set; }

        public UPrimeInfo(UPrime instance) {
            Dependencies = new List<UPrimeInfo>();
            Type = instance.GetType();
            Instance = instance;
            Assembly = Type.Assembly;
        }

        public override string ToString()
        {
            return string.Format("{0}", Type.AssemblyQualifiedName);
        }
    }
}
