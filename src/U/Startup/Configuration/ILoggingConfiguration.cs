
namespace U.Startup.Configuration
{
    /// <summary>
    /// used to configure logging system. （Unused未使用）
    /// </summary>
    public interface ILoggingConfiguration
    {
        /// <summary>
        /// gets or sets configure Log4net relative path.
        /// </summary>
        string Log4netRelativePath { get; set; }
    }
}
