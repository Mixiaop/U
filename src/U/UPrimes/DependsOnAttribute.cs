using System;

namespace U.UPrimes
{
    /// <summary>
    /// 用于定义模块之间的依赖关系
    /// 它只适用于继承UPrime的模块类
    /// </summary>
    public class DependsOnAttribute : Attribute
    {
        public Type[] DependedUPrimeTypes { get; set; }

        public DependsOnAttribute(params Type[] dependedUPrimeTypes) {
            DependedUPrimeTypes = dependedUPrimeTypes;
        }
    }
}
