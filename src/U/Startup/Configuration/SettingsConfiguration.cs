
namespace U.Startup.Configuration
{

    public class SettingsConfiguration : ISettingsConfiguration
    {
        public string SettingsPath
        {
            get;
            set;
        }

        public bool IsOpenedCachingInterceptor { get; set; }

        public bool IsOpenedApplicationLogExceptionInterceptor { get; set; }
    }
}
