using System;
using U.Logging;
using U.Startup;
using U.UPrimes;

namespace U
{
    /// <summary>
    /// 这是 U 系统核心引擎类，负责整个 U 系统的启动与销毁
    /// 你需要通过Startup()拉开火种启动系统，或Dispose来销毁它
    /// </summary>
    public class UStarter : IDisposable
    {
        #region Fields & Ctor

        /// <summary>
        /// 是否已销毁
        /// </summary>
        private bool IsDisposed;

        private IUPrimeManager _uprimeManager;

        public UPrimeEngine UPrime { get; private set; }

        public UStarter()
            : this(UPrimeEngine.Instance)
        {

        }

        public UStarter(UPrimeEngine uprime)
        {
            UPrime = uprime;
        }
        #endregion

        /// <summary>
        /// UPrime startup
        /// </summary>
        public virtual void Startup(bool debug = false)
        {
            //UPrime IocManager
            UPrimeEngine.Instance.Initialize(new DependencyCoreInstaller().Install());
            UPrimeEngine.Instance.Resolve<IUStartupConfiguration>().Initialize();
            if (!debug)
            {
                _uprimeManager = UPrimeEngine.Instance.Resolve<IUPrimeManager>();
                _uprimeManager.InitializeUPrimes();

                var logger = UPrimeEngine.Instance.Resolve<ILogger>();
                logger.Information("(^_^)(^_^)(^_^) UStarter startup completed.");
            }

        }

        /// <summary>
        /// UPrime dispose
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            IsDisposed = true;

            if (_uprimeManager != null)
            {
                _uprimeManager.ShutdownUPrimes();
            }
        }
    }
}
