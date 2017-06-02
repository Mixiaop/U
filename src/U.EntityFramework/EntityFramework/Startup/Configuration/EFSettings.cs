using U.Settings;

namespace U.EntityFramework.Startup.Configuration
{
    public class EFSettings : USettings<EFSettings>
    {
        public string SqlConnectionString { get; set; }
    }
}
