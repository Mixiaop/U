using U.Domain.Entities;

namespace U.Domain.Repositories
{
    /// <summary>
    /// <see cref="IRepository{TEntity,TPrimaryKey}"/>的泛型版本，默认使用<see cref="int"/>类型当做主键
    /// </summary>
    /// <typeparam name="TEntity">实现类型</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity<int>
    {

    }
}
