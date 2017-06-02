using U.Settings;

namespace U.Dapper.Startup.Configuration
{
    public class DapperSettings : USettings<DapperSettings>
    {
        public string SqlConnectionString { get; set; }
    }
}
