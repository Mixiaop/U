using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace U.Tests.Web.UFramework
{
    public interface IConventionalDbProvider {
        DbContext DbContext { get; }
    }

    public class ConventionalDbProvider : IConventionalDbProvider {
        public DbContext DbContext { get; private set; }
        public ConventionalDbProvider(DbContext dbContext)
        {
            DbContext = dbContext;
        }
    }

    public class ConventionalDbContext : DbContext
    {
        public virtual IDbSet<Article> Articles { get; set; }
        public ConventionalDbContext() : base("Data Source=120.132.57.7,1444;Initial Catalog=Tests;Persist Security Info=True;User ID=sa;Password=youzy.cn") { 
        
        }
    }

    public class ConventionalRepository {
        private readonly IConventionalDbProvider _dbContextProvider;
        public ConventionalRepository(IConventionalDbProvider dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public void Insert(Article article) {
            var db = _dbContextProvider.DbContext;
            var a = 1;
        }
    }
}