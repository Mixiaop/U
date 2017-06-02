using System;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using U.Dapper;
using U.Dapper.Mapper;
using U.Domain.Entities;
using U.Domain.Repositories;

namespace U.Dapper.Repositories
{
    /// <summary>
    /// Implements IRepository for dapper
    /// </summary>
    public abstract class DapperRepositoryBase<TEntity, TPrimaryKey> : IDapperRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        #region Fields & Ctor
        private readonly IDapperContextProvider _dbContext;

        public IDapperContextProvider Context
        {
            get
            {
                return _dbContext;
            }
        }

        public IDapperImplementor Dapper
        {
            get { return _dbContext.Dapper; }
        }

        public DapperRepositoryBase(IDapperContextProvider dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Get
        public virtual TEntity Get(TPrimaryKey id)
        {
            TEntity entity;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                entity = Dapper.Get<TEntity>(conn, id, null, null);
                conn.Close();
            }

            return entity;
        }

        public TEntity Get<T2>(TPrimaryKey id, Func<IClassMapper> funcMapInput, Func<TEntity, T2, TEntity> funcMapOutput)
        {
            return Get<T2>(id, funcMapInput(), funcMapOutput);
        }

        public TEntity Get<T2>(TPrimaryKey id, IClassMapper mapInput, Func<TEntity, T2, TEntity> funcMapOutput)
        {
            TEntity entity;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                entity = Dapper.Get<TEntity, T2>(conn, mapInput, funcMapOutput, id, null, null);
                conn.Close();
            }

            return entity;
        }

        public TEntity Get<T2, T3>(TPrimaryKey id, Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, TEntity> funcMapOutput)
        {
            return Get<T2, T3>(id, funcMapInput(), funcMapOutput);
        }
        public TEntity Get<T2, T3>(TPrimaryKey id, IClassMapper mapInput, Func<TEntity, T2, T3, TEntity> funcMapOutput)
        {
            TEntity entity;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                entity = Dapper.Get<TEntity, T2, T3>(conn, mapInput, funcMapOutput, id, null, null);
                conn.Close();
            }

            return entity;
        }
        public TEntity Get<T2, T3, T4>(TPrimaryKey id, Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, T4, TEntity> funcMapOutput)
        {
            return Get<T2, T3, T4>(id, funcMapInput(), funcMapOutput);
        }
        public TEntity Get<T2, T3, T4>(TPrimaryKey id, IClassMapper mapInput, Func<TEntity, T2, T3, T4, TEntity> funcMapOutput)
        {
            TEntity entity;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                entity = Dapper.Get<TEntity, T2, T3, T4>(conn, mapInput, funcMapOutput, id, null, null);
                conn.Close();
            }

            return entity;
        }
        public TEntity Get<T2, T3, T4, T5>(TPrimaryKey id, Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, T4, T5, TEntity> funcMapOutput)
        {
            return Get<T2, T3, T4, T5>(id, funcMapInput(), funcMapOutput);
        }
        public TEntity Get<T2, T3, T4, T5>(TPrimaryKey id, IClassMapper mapInput, Func<TEntity, T2, T3, T4, T5, TEntity> funcMapOutput)
        {
            TEntity entity;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                entity = Dapper.Get<TEntity, T2, T3, T4, T5>(conn, mapInput, funcMapOutput, id, null, null);
                conn.Close();
            }

            return entity;
        }
        #endregion

        #region First
        public TEntity FirstOrDefault(object predicate = null)
        {
            IList<TEntity> result;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                result = Dapper.GetList<TEntity>(conn, predicate, null, null, null, false).ToList();
                conn.Close();
            }

            if (result != null && result.Count > 0)
                return result[0];
            else
                return default(TEntity);
        }

        public TEntity FirstOrDefault<T2>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, TEntity> funcMapOutput, object predicate = null)
        {
            return FirstOrDefault<T2>(funcMapInput(), funcMapOutput, predicate);
        }
        public TEntity FirstOrDefault<T2>(IClassMapper mapInput, Func<TEntity, T2, TEntity> funcMapOutput, object predicate = null)
        {
            IList<TEntity> result;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                result = Dapper.GetList<TEntity, T2>(conn, mapInput, funcMapOutput, predicate, null, null, null, false).ToList();
                conn.Close();
            }
            if (result != null && result.Count > 0)
                return result[0];
            else
                return default(TEntity);
        }

        public TEntity FirstOrDefault<T2, T3>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, TEntity> funcMapOutput, object predicate = null)
        {
            return FirstOrDefault<T2, T3>(funcMapInput(), funcMapOutput, predicate);
        }
        public TEntity FirstOrDefault<T2, T3>(IClassMapper mapInput, Func<TEntity, T2, T3, TEntity> funcMapOutput, object predicate = null)
        {
            IList<TEntity> result;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                result = Dapper.GetList<TEntity, T2, T3>(conn, mapInput, funcMapOutput, predicate, null, null, null, false).ToList();
                conn.Close();
            }
            if (result != null && result.Count > 0)
                return result[0];
            else
                return default(TEntity);
        }
        public TEntity FirstOrDefault<T2, T3, T4>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, T4, TEntity> funcMapOutput, object predicate = null)
        {
            return FirstOrDefault<T2, T3, T4>(funcMapInput(), funcMapOutput, predicate);
        }
        public TEntity FirstOrDefault<T2, T3, T4>(IClassMapper mapInput, Func<TEntity, T2, T3, T4, TEntity> funcMapOutput, object predicate = null)
        {
            IList<TEntity> result;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                result = Dapper.GetList<TEntity, T2, T3, T4>(conn, mapInput, funcMapOutput, predicate, null, null, null, false).ToList();
                conn.Close();
            }
            if (result != null && result.Count > 0)
                return result[0];
            else
                return default(TEntity);
        }
        public TEntity FirstOrDefault<T2, T3, T4, T5>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, T4, T5, TEntity> funcMapOutput, object predicate = null)
        {
            return FirstOrDefault<T2, T3, T4, T5>(funcMapInput(), funcMapOutput, predicate);
        }
        public TEntity FirstOrDefault<T2, T3, T4, T5>(IClassMapper mapInput, Func<TEntity, T2, T3, T4, T5, TEntity> funcMapOutput, object predicate = null)
        {
            IList<TEntity> result;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                result = Dapper.GetList<TEntity, T2, T3, T4, T5>(conn, mapInput, funcMapOutput, predicate, null, null, null, false).ToList();
                conn.Close();
            }
            if (result != null && result.Count > 0)
                return result[0];
            else
                return default(TEntity);
        }
        #endregion

        #region GetList
        public IList<TEntity> GetList(object predicate = null, IList<ISort> sort = null, int count = 0)
        {
            IList<TEntity> result;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                result = Dapper.GetList<TEntity>(conn, predicate, sort, null, null, false).ToList();
                conn.Close();
            }
            return result;
        }

        public IList<TEntity> GetList<T2>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, TEntity> funcMapOutput, object predicate = null, IList<ISort> sort = null, int count = 0)
        {
            return GetList<T2>(funcMapInput(), funcMapOutput, predicate, sort);
        }
        public IList<TEntity> GetList<T2>(IClassMapper mapInput, Func<TEntity, T2, TEntity> funcMapOutput, object predicate = null, IList<ISort> sort = null, int count = 0)
        {
            IList<TEntity> result;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                result = Dapper.GetList<TEntity, T2>(conn, mapInput, funcMapOutput, predicate, sort, null, null, false).ToList();
                conn.Close();
            }
            return result;
        }

        public IList<TEntity> GetList<T2, T3>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, TEntity> funcMapOutput, object predicate = null, IList<ISort> sort = null, int count = 0)
        {
            return GetList<T2, T3>(funcMapInput(), funcMapOutput, predicate, sort);
        }
        public IList<TEntity> GetList<T2, T3>(IClassMapper mapInput, Func<TEntity, T2, T3, TEntity> funcMapOutput, object predicate = null, IList<ISort> sort = null, int count = 0)
        {
            IList<TEntity> result;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                result = Dapper.GetList<TEntity, T2, T3>(conn, mapInput, funcMapOutput, predicate, sort, null, null, false).ToList();
                conn.Close();
            }
            return result;
        }

        public IList<TEntity> GetList<T2, T3, T4>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, T4, TEntity> funcMapOutput, object predicate = null, IList<ISort> sort = null, int count = 0)
        {
            return GetList<T2, T3, T4>(funcMapInput(), funcMapOutput, predicate, sort);
        }

        public IList<TEntity> GetList<T2, T3, T4>(IClassMapper mapInput, Func<TEntity, T2, T3, T4, TEntity> funcMapOutput, object predicate = null, IList<ISort> sort = null, int count = 0)
        {
            IList<TEntity> result;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                result = Dapper.GetList<TEntity, T2, T3, T4>(conn, mapInput, funcMapOutput, predicate, sort, null, null, false).ToList();
                conn.Close();
            }
            return result;
        }
        public IList<TEntity> GetList<T2, T3, T4, T5>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, T4, T5, TEntity> funcMapOutput, object predicate = null, IList<ISort> sort = null, int count = 0)
        {
            return GetList<T2, T3, T4, T5>(funcMapInput(), funcMapOutput, predicate, sort);
        }
        public IList<TEntity> GetList<T2, T3, T4, T5>(IClassMapper mapInput, Func<TEntity, T2, T3, T4, T5, TEntity> funcMapOutput, object predicate = null, IList<ISort> sort = null, int count = 0)
        {
            IList<TEntity> result;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                result = Dapper.GetList<TEntity, T2, T3, T4, T5>(conn, mapInput, funcMapOutput, predicate, sort, null, null, false).ToList();
                conn.Close();
            }
            return result;
        }
        #endregion

        #region GetPage
        public IList<TEntity> GetPage(int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null)
        {
            IList<TEntity> result;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                result = Dapper.GetPage<TEntity>(conn, predicate, sort, pageIndex, pageSize, null, null, false).ToList();
                conn.Close();
            }

            return result;
        }

        public IList<TEntity> GetPage<T2>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, TEntity> funcMapOutput, int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null)
        {
            return GetPage<T2>(funcMapInput(), funcMapOutput, pageIndex, pageSize, predicate, sort);
        }
        public IList<TEntity> GetPage<T2>(IClassMapper mapInput, Func<TEntity, T2, TEntity> funcMapOutput, int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null)
        {
            IList<TEntity> result;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                result = Dapper.GetPage<TEntity, T2>(conn, mapInput, funcMapOutput, predicate, sort, pageIndex, pageSize, null, null, false).ToList();
                conn.Close();
            }
            return result;
        }

        public IList<TEntity> GetPage<T2, T3>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, TEntity> funcMapOutput, int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null)
        {
            return GetPage<T2, T3>(funcMapInput(), funcMapOutput, pageIndex, pageSize, predicate, sort);
        }
        public IList<TEntity> GetPage<T2, T3>(IClassMapper mapInput, Func<TEntity, T2, T3, TEntity> funcMapOutput, int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null)
        {
            IList<TEntity> result;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                result = Dapper.GetPage<TEntity, T2, T3>(conn, mapInput, funcMapOutput, predicate, sort, pageIndex, pageSize, null, null, false).ToList();
                conn.Close();
            }
            return result;
        }

        public IList<TEntity> GetPage<T2, T3, T4>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, T4, TEntity> funcMapOutput, int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null)
        {
            return GetPage<T2, T3, T4>(funcMapInput(), funcMapOutput, pageIndex, pageSize, predicate, sort);
        }
        public IList<TEntity> GetPage<T2, T3, T4>(IClassMapper mapInput, Func<TEntity, T2, T3, T4, TEntity> funcMapOutput, int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null)
        {
            IList<TEntity> result;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                result = Dapper.GetPage<TEntity, T2, T3, T4>(conn, mapInput, funcMapOutput, predicate, sort, pageIndex, pageSize, null, null, false).ToList();
                conn.Close();
            }
            return result;
        }
        public IList<TEntity> GetPage<T2, T3, T4, T5>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, T4, T5, TEntity> funcMapOutput, int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null)
        {
            return GetPage<T2, T3, T4, T5>(funcMapInput(), funcMapOutput, pageIndex, pageSize, predicate, sort);
        }
        public IList<TEntity> GetPage<T2, T3, T4, T5>(IClassMapper mapInput, Func<TEntity, T2, T3, T4, T5, TEntity> funcMapOutput, int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null)
        {
            IList<TEntity> result;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                result = Dapper.GetPage<TEntity, T2, T3, T4, T5>(conn, mapInput, funcMapOutput, predicate, sort, pageIndex, pageSize, null, null, false).ToList();
                conn.Close();
            }
            return result;
        }
        #endregion

        #region Count
        public int Count(object predicate = null)
        {
            int count = 0;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                count = Dapper.Count<TEntity>(conn, predicate, null, null);
                conn.Close();
            }

            return count;
        }

        public int Count(Func<IClassMapper> funcMapInput, object predicate = null)
        {
            return Count(funcMapInput(), predicate);
        }

        public int Count(IClassMapper mapInput, object predicate = null)
        {
            int count = 0;
            using (var conn = Context.GetConnection(true))
            {
                conn.Open();
                count = Dapper.Count<TEntity>(conn, mapInput, predicate, null, null);
                conn.Close();
            }

            return count;
        }
        #endregion

        #region CUD
        public TEntity Insert(TEntity entity)
        {
            using (var conn = Context.GetConnection())
            {
                conn.Open();
                entity.Id = Dapper.Insert<TEntity>(conn, entity, null, null);
                conn.Close();
            }
            return entity;

        }

        public TEntity Update(TEntity entity)
        {
            using (var conn = Context.GetConnection())
            {
                conn.Open();
                Dapper.Update<TEntity>(conn, entity, null, null);
                conn.Close();
            }
            return entity;
        }

        public void Delete(TEntity entity)
        {
            using (var conn = Context.GetConnection())
            {
                conn.Open();
                Dapper.Delete<TEntity>(conn, entity, null, null);
                conn.Close();
            }
        }

        public void Delete(object predicate = null)
        {
            using (var conn = Context.GetConnection())
            {
                conn.Open();
                Dapper.Delete<TEntity>(conn, predicate, null, null);
                conn.Close();
            }
        }
        #endregion

        #region Async Methods
        //public Task<TEntity> GetAsync(TPrimaryKey id)
        //{
        //    return Task.FromResult(Get(id));
        //}

        //public async Task<TEntity> InsertAsync(TEntity entity)
        //{
        //    entity.Id = await Database.Insert<TEntity>(entity);
        //    return entity;
        //}

        //public Task<TEntity> UpdateAsync(TEntity entity) {
        //    return Task.FromResult(Update(entity));
        //}

        //public Task DeleteAsync(TEntity entity)
        //{
        //    Delete(entity);
        //    return Task.FromResult(0);
        //} 
        #endregion

        public IDbConnection DbConnection
        {
            get
            {
                return Context.GetConnection();
            }
        }
    }
}
