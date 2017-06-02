using System.Collections.Generic;
using System.Data.Entity;
using U.Domain.Entities;

namespace U.EntityFramework
{
    /// <summary>
    /// EF 的 DbContext 扩展接口定义
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// 保存并提交
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// 执行存储过程，并返回加载的实现列表
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="commandText">命令</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
            where TEntity : IEntity, new();

        /// <summary>
        /// 创建一个原始SQL查询将返回给定泛型类型的元素
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);

        /// <summary>
        /// 对数据库执行给定的DDL和DML命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="doNotEnsureTransaction"></param>
        /// <param name="timeout"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

        /// <summary>
        /// 分离实体
        /// </summary>
        /// <param name="entity"></param>
        void Detach(object entity);

        /// <summary>
        /// 获取 DbContext
        /// </summary>
        UDbContext Current { get; }

        /// <summary>
        /// 获取或设置一个值，指示是否启用了【代理创建设置】
        /// </summary>
        bool ProxyCreationEnabled { get; set; }

        /// <summary>
        ///获取或设置一个值，指示是否启用了【自动检测设置更改】
        /// </summary>
        bool AutoDetectChangesEnabled { get; set; }
    }
}
