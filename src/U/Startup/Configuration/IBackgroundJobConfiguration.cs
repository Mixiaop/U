
namespace U.Startup.Configuration
{
    /// <summary>
    /// used to configure background job systems.
    /// </summary>
    public interface IBackgroundJobConfiguration
    {
        /// <summary>
        /// Gets the ABP configuration object.
        /// </summary>
        IUStartupConfiguration UConfiguration { get; }

        /// <summary>
        /// Used to enable/disable background job execution.
        /// </summary>
        bool IsJobExecutionEnabled { get; set; }
    }
}
