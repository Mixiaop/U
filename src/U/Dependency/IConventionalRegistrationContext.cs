using System.Reflection;

namespace U.Dependency
{
    /// <summary>
    /// 用于传递需要进行约定注册处理的对象（这里指传递处理继承ISingletonDependency及ITransientDependency的类）
    /// </summary>
    public interface  IConventionalRegistrationContext
    {
        /// <summary>
        /// 获取注册的程序集
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Ioc manager
        /// </summary>
        IIocManager IocManager { get; }
    }
}
