using System;
using U.Utilities.Configuration;

namespace U.Utilities.SqlServer
{
    /// <summary>
    /// 数据库连接配置文件信息
    /// </summary>
    [Serializable]
    public class DatabaseConfigInfo : IConfigInfo
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DbConnectinString { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DbType { get; set; }
    }
}
