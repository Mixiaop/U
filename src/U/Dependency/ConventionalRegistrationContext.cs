using System.Reflection;

namespace U.Dependency
{
    /// <summary>
    /// 注册器上下文
    /// 此类是用于传递自定义注册器的对象
    /// 如 BasicConventionalRegistrar
    /// </summary>
    public class ConventionalRegistrationContext : IConventionalRegistrationContext
    {
        public Assembly Assembly
        {
            get;
            private set;
        }

        public IIocManager IocManager
        {
            get;
            private set;
        }

        public ConventionalRegistrationContext(Assembly assembly, IIocManager iocManager)
        {
            Assembly = assembly;
            IocManager = iocManager;
        }
    }
}
