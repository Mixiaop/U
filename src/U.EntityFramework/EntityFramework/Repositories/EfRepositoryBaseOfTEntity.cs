using System.Data.Entity;
using U.Domain.Entities;
using U.Domain.Repositories;

namespace U.EntityFramework.Repositories
{
    public class EfRepositoryBase<TDbContext, TEntity> : EfRepositoryBase<TDbContext, TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
        where TDbContext : UDbContext
    {
        public EfRepositoryBase(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
