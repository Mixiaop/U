using U.Settings;

namespace U.Tests.Web.Infrastructure
{
    [USettingsPathArribute("TestSettings.json","/Config/")]
    public class TestSettings : USettings<TestSettings>
    {
        public string Name { get; set; }
    }
}