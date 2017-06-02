using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using U.Domain.Entities;
using U.Domain.Repositories;
using U.EntityFramework;
using U.EntityFramework.Repositories;

namespace U.Tests.Web.EntityFramework
{
    [Table("Areas")]
    public class Area : Entity, ISoftDelete
    {
        public string AreaName { get; set; }
        public bool IsDeleted { get; set; }
    }
    [Table("Animal")]
    public class Animal : Entity {

        public string Name { get; set; }
    }

    public interface IAreaRepository : IRepository<Area> { }

    public class AreaRepository : MyModuleRepositoryBase<Area>, IAreaRepository {
        public AreaRepository(MyContext dbContextProvider) : base(dbContextProvider) { 
            
        }
    }

    public interface IAnimalRepository : IRepository<Animal> { 
    
    }

    public class AnimalRepository : MyModuleRepositoryBase<Animal>, IAnimalRepository {
        public AnimalRepository(MyContext dbContextProvider)
            : base(dbContextProvider)
        { 
            
        }
    }

    public abstract class MyModuleRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<MyContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected MyModuleRepositoryBase(MyContext dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }

    public abstract class MyModuleRepositoryBase<TEntity> : EfRepositoryBase<MyContext, TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected MyModuleRepositoryBase(MyContext dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }

    public class MyContext : UDbContext
    {
        public virtual IDbSet<Area> Areas { get; set; }

        public virtual IDbSet<Animal> Animals { get; set; }

        public MyContext() : this("") { }
        public MyContext(string nameOrConnectionString)
            : base("Data Source=120.132.57.7,1444;Initial Catalog=Tests;Persist Security Info=True;User ID=sa;Password=youzy.cn")
        { 
        
        }
    }
}