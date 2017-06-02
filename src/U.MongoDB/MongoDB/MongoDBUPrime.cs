using System.Reflection;
using U.UPrimes;
using U.MongoDB.Startup.Configuration;

namespace U.MongoDB
{
    public class MongoDbUPrime : UPrime
    {
        public override void PreInitialize()
        {
            Engine.IocManager.Register<IMongoDbConfiguration, MongoDbConfiguration>();
        }

        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
