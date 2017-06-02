using System;
using U.Utilities.Configuration;

namespace U.Utilities.Access
{
    /// <summary>
    /// 数据库连接配置文件信息
    /// </summary>
    [Serializable]
    public class DatabaseConfigInfo : IConfigInfo
    {
        /// <summary>
        /// 数据库路径
        /// </summary>
        public string DbPath { get; set; }
    }
}
