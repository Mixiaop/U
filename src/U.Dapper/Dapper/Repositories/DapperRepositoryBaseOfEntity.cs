using U.Domain.Entities;
using U.Domain.Repositories;

namespace U.Dapper.Repositories
{
    /// <summary>
    /// Implements IRepository for dapper
    /// </summary>
    public abstract class DapperRepositoryBase<TEntity> : DapperRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        public DapperRepositoryBase(IDapperContextProvider dbContext)
            : base(dbContext)
        {}
    }
}
