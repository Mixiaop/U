using System;
using System.Collections.Generic;
using System.Data;
using U.Dapper.Mapper;
using U.Domain.Entities;
using U.Domain.Repositories;

namespace U.Dapper.Repositories
{
    public interface IDapperRepository<TEntity, TPrimaryKey> : IRepository
        where TEntity : class, IEntity<TPrimaryKey>
    {
        TEntity Get(TPrimaryKey id);
        TEntity Get<T2>(TPrimaryKey id, Func<IClassMapper> funcMapInput, Func<TEntity, T2, TEntity> funcMapOutput);
        TEntity Get<T2>(TPrimaryKey id, IClassMapper mapInput, Func<TEntity, T2, TEntity> funcMapOutput);
        TEntity Get<T2, T3>(TPrimaryKey id, Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, TEntity> funcMapOutput);
        TEntity Get<T2, T3>(TPrimaryKey id, IClassMapper mapInput, Func<TEntity, T2, T3, TEntity> funcMapOutput);
        TEntity Get<T2, T3, T4>(TPrimaryKey id, Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, T4, TEntity> funcMapOutput);
        TEntity Get<T2, T3, T4>(TPrimaryKey id, IClassMapper mapInput, Func<TEntity, T2, T3, T4, TEntity> funcMapOutput);
        TEntity Get<T2, T3, T4, T5>(TPrimaryKey id, Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, T4, T5, TEntity> funcMapOutput);
        TEntity Get<T2, T3, T4, T5>(TPrimaryKey id, IClassMapper mapInput, Func<TEntity, T2, T3, T4, T5, TEntity> funcMapOutput);

        TEntity FirstOrDefault(object predicate = null);
        TEntity FirstOrDefault<T2>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, TEntity> funcMapOutput, object predicate = null);
        TEntity FirstOrDefault<T2>(IClassMapper mapInput, Func<TEntity, T2, TEntity> funcMapOutput, object predicate = null);
        TEntity FirstOrDefault<T2, T3>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, TEntity> funcMapOutput, object predicate = null);
        TEntity FirstOrDefault<T2, T3>(IClassMapper mapInput, Func<TEntity, T2, T3, TEntity> funcMapOutput, object predicate = null);
        TEntity FirstOrDefault<T2, T3, T4>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, T4, TEntity> funcMapOutput, object predicate = null);
        TEntity FirstOrDefault<T2, T3, T4>(IClassMapper mapInput, Func<TEntity, T2, T3, T4, TEntity> funcMapOutput, object predicate = null);
        TEntity FirstOrDefault<T2, T3, T4, T5>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, T4, T5, TEntity> funcMapOutput, object predicate = null);
        TEntity FirstOrDefault<T2, T3, T4, T5>(IClassMapper mapInput, Func<TEntity, T2, T3, T4, T5, TEntity> funcMapOutput, object predicate = null);


        IList<TEntity> GetList(object predicate = null, IList<ISort> sort = null, int count = 0);
        IList<TEntity> GetList<T2>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, TEntity> funcMapOutput, object predicate = null, IList<ISort> sort = null, int count = 0);
        IList<TEntity> GetList<T2>(IClassMapper mapInput, Func<TEntity, T2, TEntity> funcMapOutput, object predicate = null, IList<ISort> sort = null, int count = 0);
        IList<TEntity> GetList<T2, T3>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, TEntity> funcMapOutput, object predicate = null, IList<ISort> sort = null, int count = 0);
        IList<TEntity> GetList<T2, T3>(IClassMapper mapInput, Func<TEntity, T2, T3, TEntity> funcMapOutput, object predicate = null, IList<ISort> sort = null, int count = 0);
        IList<TEntity> GetList<T2, T3, T4>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, T4, TEntity> funcMapOutput, object predicate = null, IList<ISort> sort = null, int count = 0);
        IList<TEntity> GetList<T2, T3, T4>(IClassMapper mapInput, Func<TEntity, T2, T3, T4, TEntity> funcMapOutput, object predicate = null, IList<ISort> sort = null, int count = 0);
        IList<TEntity> GetList<T2, T3, T4, T5>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, T3, T4, T5, TEntity> funcMapOutput, object predicate = null, IList<ISort> sort = null, int count = 0);
        IList<TEntity> GetList<T2, T3, T4, T5>(IClassMapper mapInput, Func<TEntity, T2, T3, T4, T5, TEntity> funcMapOutput, object predicate = null, IList<ISort> sort = null, int count = 0);

        IList<TEntity> GetPage(int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null);
        IList<TEntity> GetPage<T2>(Func<IClassMapper> funcMapInput, Func<TEntity, T2, TEntity> funcMapOutput, int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null);
        IList<TEntity> GetPage<T2>(IClassMapper mapInput, Func<TEntity, T2, TEntity> funcMapOutput, int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null);
        IList<TEntity> GetPage<T2, T3>(Func<IClassMapper> mapInput, Func<TEntity, T2, T3, TEntity> funcMapOutput, int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null);
        IList<TEntity> GetPage<T2, T3>(IClassMapper mapInput, Func<TEntity, T2, T3, TEntity> funcMapOutput, int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null);
        IList<TEntity> GetPage<T2, T3, T4>(Func<IClassMapper> mapInput, Func<TEntity, T2, T3, T4, TEntity> funcMapOutput, int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null);
        IList<TEntity> GetPage<T2, T3, T4>(IClassMapper mapInput, Func<TEntity, T2, T3, T4, TEntity> funcMapOutput, int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null);
        IList<TEntity> GetPage<T2, T3, T4, T5>(Func<IClassMapper> mapInput, Func<TEntity, T2, T3, T4, T5, TEntity> funcMapOutput, int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null);
        IList<TEntity> GetPage<T2, T3, T4, T5>(IClassMapper mapInput, Func<TEntity, T2, T3, T4, T5, TEntity> funcMapOutput, int pageIndex, int pageSize, object predicate = null, IList<ISort> sort = null);

        int Count(object predicate = null);
        int Count(Func<IClassMapper> funcMapInput, object predicate = null);
        int Count(IClassMapper mapInput, object predicate = null);

        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(object predicate = null);

        IDbConnection DbConnection { get; }
    }
}
