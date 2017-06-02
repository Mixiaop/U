
namespace U.Logging
{
    /// <summary>
    /// This class can be used to write logs from somewhere where it's a hard to get a reference to the <see cref="ILogger"/>.
    /// Normally, use <see cref="ILogger"/> with property injection wherever it's possible.
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// A reference to the logger.
        /// </summary>
        public static ILogger Logger { get; private set; }

        static LogHelper()
        {
            Logger = UPrimeEngine.Instance.IocManager.IsRegistered(typeof(ILogger))
                ? UPrimeEngine.Instance.IocManager.Resolve<ILogger>()
                : NullLogger.Instance;
        }
    }
}
