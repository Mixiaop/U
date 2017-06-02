
namespace U.Startup.Configuration
{
    /// <summary>
    /// used to configure settings system.
    /// </summary>
    public interface ISettingsConfiguration
    {
        /// <summary>
        /// Gets or sets settings storage files path.
        /// </summary>
        string SettingsPath { get; set; }
    }
}
