using System;
using System.Collections.Generic;
using System.Reflection;
using U.Dapper.Mapper;
using U.Dapper.Sql;

namespace U.Dapper.Startup.Configuration
{
    public interface IDapperConfiguration
    {
        Type DefaultMapper { get; }
        IList<Assembly> MappingAssemblies { get; }
        ISqlDialect Dialect { get; }
        IClassMapper GetMap(Type entityType);
        IClassMapper GetMap<T>() where T : class;
        void ClearCache();
        Guid GetNextGuid();

        #region Connection String
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
        #endregion
    }
}
