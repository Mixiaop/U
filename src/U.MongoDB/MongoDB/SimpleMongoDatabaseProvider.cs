using MongoDB.Driver;
using U.Dependency;
using U.MongoDB.Startup.Configuration;

namespace U.MongoDB
{
    public class SimpleMongoDatabaseProvider : IMongoDatabaseProvider, ITransientDependency
    {
        public IMongoDatabase Database { get; private set; }

        private readonly IMongoDbConfiguration _configuration;

        public SimpleMongoDatabaseProvider(IMongoDbConfiguration mongoDBConfiguration) {
            _configuration = mongoDBConfiguration;
            var client = new MongoClient(_configuration.ConnectionString);
            Database = client.GetDatabase(_configuration.DatabaseName);
        }
    }
}
