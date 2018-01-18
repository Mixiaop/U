using U.Logging;
using U.Dependency;
using U.Startup;

namespace U
{
    /// <summary>
    /// U 引擎，IOC容器、日志、配置等应用
    /// </summary>
    public class UPrimeEngine
    {
        #region Fields & Ctor
        private bool _initialized = false;

        public static UPrimeEngine Instance { get; private set; }

        public IIocManager IocManager { get; private set; }

        public IUStartupConfiguration Configuration { get; private set; }

        public ILogger Logger { get; private set; }
        static UPrimeEngine()
        {
            Instance = new UPrimeEngine();
        }
        #endregion

        #region Consts
        public const string Log4netRelativePath = "Config/U/log4net.Config";
        #endregion

        public void Initialize(IIocManager iocManage)
        {
            if (!_initialized)
            {
                Logger = iocManage.Resolve<ILogger>();
                IocManager = iocManage;
                Configuration = IocManager.Resolve<IUStartupConfiguration>();
                _initialized = true;
            }
            else
            {
                throw new UException("UPrime has been initialized!");
            }
        }

        #region Ioc Methods
        public void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton, string namedAlias = "") where T : class
        {
            IocManager.Register(typeof(T), typeof(T), lifeStyle, namedAlias);
        }

        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton, string namedAlias = "")
        {
            IocManager.Register(typeof(TType), typeof(TImpl), lifeStyle, namedAlias);
        }


        public T Resolve<T>(string namedAlias = "") where T : class
        {
            return IocManager.Resolve<T>(namedAlias);
        }

        public T ResolveUnregistered<T>() where T : class
        {
            return IocManager.ResolveUnregistered<T>();
        }
        #endregion
    }
}
