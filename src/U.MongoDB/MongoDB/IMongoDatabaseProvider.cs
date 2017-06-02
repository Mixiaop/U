using MongoDB.Driver;

namespace U.MongoDB
{
    /// <summary>
    /// 定义 Mongo 的数据库提供器
    /// </summary>
    public interface IMongoDatabaseProvider
    {
        IMongoDatabase Database { get; }
    }
}
