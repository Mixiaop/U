using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using U.Domain.Entities;
using U.Domain.Repositories;

namespace U.EntityFramework.Repositories
{
    /// <summary>
    /// 使用 Entity Framework 实现 IRepository 仓储接口
    /// </summary>
    /// <typeparam name="TDbContext">指定实体 <see cref="TEntity"/> 的 DbContext （EF特性）</typeparam>
    /// <typeparam name="TEntity">指定仓储的实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">仓储的实体Id类型</typeparam>
    public class EfRepositoryBase<TDbContext, TEntity, TPrimaryKey> : URepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {

        #region Fields & Properties
        private readonly bool _openAsNoTracking = true;
        private UDbContext _dbContext;
        private IDbSet<TEntity> _entities;

        public virtual UDbContext Context
        {
            get { return _dbContext; }
            set { _dbContext = value; }
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table
        {
            get
            {
                //if (_openAsNoTracking)
                //{
                //    return this.Entities.AsNoTracking();
                //}
                //else {
                //    return this.Entities;
                //}
                return this.Entities;
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        //public virtual IQueryable<TEntity> TableNoTracking
        //{
        //    get
        //    {
        //        return this.Entities.AsNoTracking();
        //    }
        //}

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual IDbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = ((UDbContext)this.Context).Set<TEntity>();
                return _entities;
            }
        }
        #endregion

        #region Ctor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public EfRepositoryBase(IDbContext dbContext, bool openAsNoTracking = true)
        {
            _dbContext = (UDbContext)dbContext;
            _openAsNoTracking = openAsNoTracking;
        }
        #endregion

        public override IQueryable<TEntity> GetAll()
        {
            return Table;
        }

        public override async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public override async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public override async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }

        public override async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return await GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        public override async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        public override TEntity Insert(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Add(entity);
                this.Context.SaveChanges();
                return entity;
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public override Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public override TPrimaryKey InsertAndGetId(TEntity entity)
        {
            entity = Insert(entity);

            return entity.Id;
        }

        public override async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            entity = await InsertAsync(entity);

            return entity.Id;
        }

        public override TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            entity = InsertOrUpdate(entity);

            return entity.Id;
        }

        public override async Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            entity = await InsertOrUpdateAsync(entity);

            return entity.Id;
        }

        public override TEntity Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                this.Context.Entry(entity).State = EntityState.Modified;
                this.Context.SaveChanges();

                return entity;
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public override Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        public override void Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                if (entity is ISoftDelete)
                {
                    (entity as ISoftDelete).IsDeleted = true;
                    Update(entity);
                }
                else
                {
                    //((UDbContext)this.DbContext).Entry(entity).State = EntityState.Deleted;
                    this.Entities.Remove(entity);
                    this.Context.SaveChanges();
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public override void Delete(TPrimaryKey id)
        {
            //var entity = Entities.FirstOrDefault(ent =>  EqualityComparer<TPrimaryKey>.Default.Equals(ent.Id, id));
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                entity = FirstOrDefault(id);
                if (entity == null)
                {
                    return;
                }
            }

            if (entity != null)
                Delete(entity);
        }

        public override async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }

        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }

        public override async Task<long> LongCountAsync()
        {
            return await GetAll().LongCountAsync();
        }

        public override async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).LongCountAsync();
        }


        #region Utilities

        /// <summary>
        /// Get full error
        /// </summary>
        /// <param name="exc">Exception</param>
        /// <returns>Error</returns>
        protected string GetFullErrorText(DbEntityValidationException exc)
        {
            var msg = string.Empty;
            foreach (var validationErrors in exc.EntityValidationErrors)
                foreach (var error in validationErrors.ValidationErrors)
                    msg += string.Format("Property: {0} Error: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
            return msg;
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!Entities.Local.Contains(entity))
            {
                Entities.Attach(entity);
            }
        }
        #endregion
    }
}
