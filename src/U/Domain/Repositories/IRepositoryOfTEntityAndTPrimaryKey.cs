using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using U.Domain.Entities;

namespace U.Domain.Repositories
{
    /// <summary>
    /// 此接口定义了仓储的通用方法，确保所有仓储实现此类的方法
    /// </summary>
    /// <typeparam name="TEntity">仓储的实现类型</typeparam>
    /// <typeparam name="TPrimaryKey">仓储的主键类型</typeparam>
    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>
    {
        #region Select/Get/Query

        /// <summary>
        /// 用于获取IQueryable，它可以从实体表检索实现。
        /// <see cref="UnitOfWorkAttribute"/> attribute must be used to be able to call this method since this method
        /// </summary>
        /// <returns>IQueryable 用于从数据库查询实体</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 用于获取所有实体
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAllList();

        /// <summary>
        /// 用于获取所有实体
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllListAsync();

        /// <summary>
        /// 用于获取所有实体，基于给予的 <paramref name="predicate"/> 条件。
        /// </summary>
        /// <param name="predicate">过虑实体的条件</param>
        /// <returns></returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 用于获取所有实体，基于给予的 <paramref name="predicate"/> 条件。
        /// </summary>
        /// <param name="predicate">过虑实体的条件</param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 用于运行实体集合上的一个查询。
        /// 此操作不一定需要<see cref="UnitOfWorkAttribute"/> 属性 (与 <see cref="GetAll"/>操作相反)
        /// 除非 <paramref name="queryMethod"/> 完成 IQueryable、ToList、FirstOrDefault 等。
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="queryMethod">实体上的查询</param>
        /// <returns></returns>
        T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);

        /// <summary>
        /// 通过给定的主键获取实体（如果获取不到会抛出异常）
        /// </summary>
        /// <param name="id">实体的主键Id</param>
        /// <returns></returns>
        TEntity Get(TPrimaryKey id);

        /// <summary>
        /// 通过给定的主键获取实体（如果获取不到会抛出异常）
        /// </summary>
        /// <param name="id">实体的主键Id</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(TPrimaryKey id);

        /// <summary>
        /// 通过给定的主键获取实体（如果获取不到则会返回null）
        /// </summary>
        /// <param name="id">实体的主键Id</param>
        /// <returns></returns>
        TEntity FirstOrDefault(TPrimaryKey id);

        /// <summary>
        /// 通过给定的主键获取实体（如果获取不到则会返回null）
        /// </summary>
        /// <param name="id">实体的主键Id</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);


        /// <summary>
        /// 获取实体，基于给予的 <paramref name="predicate"/> 条件。（如果获取不到则会返回null）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取实体，基于给予的 <paramref name="predicate"/> 条件。（如果获取不到则会返回null）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region Insert

        /// <summary>
        /// 插入一个新实体
        /// </summary>
        /// <param name="entity">需要插入的实体</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// 插入一个新实体
        /// </summary>
        /// <param name="entity">需要插入的实体</param>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// 插入一个新实体并返回主键Id。（一般为数据库自增）
        /// 它可能需要保存当前的UOW来返回Id
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体的Id</returns>
        TPrimaryKey InsertAndGetId(TEntity entity);

        /// <summary>
        /// 插入一个新实体并返回主键Id。（一般为数据库自增）
        /// 它可能需要保存当前的UOW来返回Id
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体的Id</returns>
        Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);
        #endregion

        #region Update

        /// <summary>
        /// 更新现有的实体
        /// </summary>
        /// <param name="entity">实体</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 更新现有的实体
        /// </summary>
        /// <param name="entity">实体</param>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// 更新现有的实体（通过Id与方法委托直接改变实体的值）
        /// </summary>
        /// <param name="id">实体的Id</param>
        /// <param name="updateAction">Action（方法委托）能被用于改变实体的值</param>
        /// <returns></returns>
        TEntity Update(TPrimaryKey id, Action<TEntity> updateAction);

        /// <summary>
        /// 更新现有的实体（通过Id与方法委托直接改变实体的值）
        /// </summary>
        /// <param name="id">实体的Id</param>
        /// <param name="updateAction">Action（方法委托）能被用于改变实体的值</param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction);

        #endregion

        #region Delete

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="entity">需要删除的</param>
        void Delete(TEntity entity);

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="entity">需要删除的</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// 通过主键删除一个实体
        /// </summary>
        /// <param name="id">实体的主键Id</param>
        void Delete(TPrimaryKey id);

        /// <summary>
        /// 通过主键删除一个实体
        /// </summary>
        /// <param name="id">实体的主键Id</param>
        Task DeleteAsync(TPrimaryKey id);

        /// <summary>
        /// 通过<see cref="Expression<Func<TEntity, bool>>"/> 删除多个实体。
        /// 注意: 删除通过条件检索出来的实体。
        /// 这里可能会存在性能问题，如果通过条件返回出来的实体太多。
        /// （因为通过foreach单条Delete，如果有大批量建议使用自定义仓储的方法优化删除）
        /// </summary>
        /// <param name="predicate">检索实体的条件</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 通过<see cref="Expression<Func<TEntity, bool>>"/> 删除多个实体。
        /// 注意: 删除通过条件检索出来的实体。
        /// 这里可能会存在性能问题，如果通过条件返回出来的实体太多。
        /// （因为通过foreach单条Delete，如果有大批量建议使用自定义仓储的方法优化删除）
        /// </summary>
        /// <param name="predicate">检索实体的条件</param>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Aggregates

        /// <summary>
        /// 获取所有实体的数量
        /// </summary>
        /// <returns>实体的数量</returns>
        int Count();

        /// <summary>
        /// 获取所有实体的数量
        /// </summary>
        /// <returns>实体的数量</returns>
        Task<int> CountAsync();

        /// <summary>
        ///获取所有实体的数量,基于给予的条件 <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">过滤数量的方法条件</param>
        /// <returns>实体数量</returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///获取所有实体的数量,基于给予的条件 <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">过滤数量的方法条件</param>
        /// <returns>实体数量</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取所有实体的数量 (如果超出来int的最大值 <see cref="int.MaxValue"/>)
        /// </summary>
        /// <returns>实体的数量</returns>
        long LongCount();

        /// <summary>
        /// 获取所有实体的数量 (如果超出来int的最大值 <see cref="int.MaxValue"/>)
        /// </summary>
        /// <returns>实体的数量</returns>
        Task<long> LongCountAsync();

        /// <summary>
        ///获取所有实体的数量,基于给予的条件 <paramref name="predicate"/>.(如果超出来int的最大值 <see cref="int.MaxValue"/>)
        /// </summary>
        /// <param name="predicate">过滤数量的方法条件</param>
        /// <returns>实体数量</returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///获取所有实体的数量,基于给予的条件 <paramref name="predicate"/>.(如果超出来int的最大值 <see cref="int.MaxValue"/>)
        /// </summary>
        /// <param name="predicate">过滤数量的方法条件</param>
        /// <returns>实体数量</returns>
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion
    }
}
