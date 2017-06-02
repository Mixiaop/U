using U.Settings;

namespace U.Hangfire.Startup.Configuration
{
    public class HangfireSettings : USettings<HangfireSettings>
    {
        /// <summary>
        /// SqlServer connection string
        /// </summary>
        public string SqlServerConn { get; set; }
    }
}
