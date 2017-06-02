using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using U.Domain.Entities;
using U.EntityFramework;
using U.EntityFramework.Repositories;

namespace U.Tests.Web.UFramework
{

    /// <summary>
    /// 自定义的抽象仓储基类
    /// </summary>
    public abstract class TestsRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<TestsDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected TestsRepositoryBase(IDbContextProvider<TestsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }

    /// <summary>
    /// 自定义的抽象仓储基类
    /// </summary>
    public abstract class TestsRepositoryBase<TEntity> : EfRepositoryBase<TestsDbContext, TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected TestsRepositoryBase(IDbContextProvider<TestsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
}