
namespace U.EntityFramework.Startup.Configuration
{
    public interface IEFConfiguration
    {
        /// <summary>
        /// 是否开启读写分离
        /// defaut:false
        /// </summary>
        bool OpenedReadAndWrite { get; }

        /// <summary>
        /// 默认数据库连接字符串（读写分离开启的情况下则为写连接字符串）
        /// </summary>
        string SqlConnectionString { get; }

        /// <summary>
        /// 读写分离开启的情况下则为读连接字符串
        /// </summary>
        string ReadSqlConnectionString { get; }
    }
}
