
namespace U.MongoDB.Startup.Configuration
{
    public class MongoDbSettings : U.Settings.USettings<MongoDbSettings>
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
