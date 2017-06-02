
namespace U.MongoDB.Startup.Configuration
{
    public class MongoDbConfiguration : IMongoDbConfiguration
    {
        public MongoDbConfiguration(MongoDbSettings settings) {
            ConnectionString = settings.ConnectionString;
            DatabaseName = settings.DatabaseName;
        }
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
