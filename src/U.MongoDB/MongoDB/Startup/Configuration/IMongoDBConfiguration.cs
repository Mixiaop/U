
namespace U.MongoDB.Startup.Configuration
{
    /// <summary>
    /// MongoDB数据库默认配置
    /// </summary>
    public interface IMongoDbConfiguration
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
