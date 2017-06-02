using System;
using System.Reflection;
using System;
using System.Data.Entity;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using U.Reflection;
using U.Dependency;
using U.Domain.Entities;
using U.Domain.Repositories;
using U.Domain.Uow;
using U.EntityFramework;
using U.EntityFramework.Uow;
using U.EntityFramework.Startup.Configuration;
using U.EntityFramework.Startup.Dependecy;
using U.EntityFramework.Repositories;

namespace U.Tests.EntityFramework.Repositories
{
    [TestClass]
    public class EntityFrameworkGenericRepositoryRegistrar_Tests : EntityFrameworkTestBase
    {
        public EntityFrameworkGenericRepositoryRegistrar_Tests()
        {

        }

        [TestMethod]
        public void Should_Resolve_Generic_Repositories()
        {
            UPrimeEngine.Instance.Register<IEFConfiguration, MyEFConfiguration>();
            new EntityFrameworkConventionalRegistrar().RegisterAssembly(
                new ConventionalRegistrationContext(
                        Assembly.GetExecutingAssembly(),
                        UPrimeEngine.Instance.IocManager
                        )

                );

            var builder = new ContainerBuilder();
            //builder.RegisterGeneric(typeof(UnitOfWorkDbContextProvider<>)).As(typeof(IDbContextProvider<>)).InstancePerLifetimeScope();
            //builder.Update(UPrimeEngine.Instance.IocManager.IocContainer);

            UPrimeEngine.Instance.Register<ICurrentUnitOfWorkProvider, CallContextCurrentUnitOfWorkProvider>();

            //var textContext = UPrimeEngine.Instance.Resolve<IDbContextProvider<MyModuleDbContext>>();

            //EntityFrameworkGenericRepositoryRegistrar.RegisterForDbContext(typeof(MyModuleDbContext), UPrimeEngine.Instance.IocManager);
            //EntityFrameworkGenericRepositoryRegistrar.RegisterForDbContext(typeof(MyMainDbContext), UPrimeEngine.Instance.IocManager);

            var typeFinder = UPrimeEngine.Instance.Resolve<ITypeFinder>();

            var dbContextTypes =
                typeFinder.Find(type =>
                    type.IsPublic &&
                    !type.IsAbstract &&
                    type.IsClass &&
                    typeof(UDbContext).IsAssignableFrom(type)
                    );

            ////Console.WriteLine(dbContextTypes.Length);
            foreach (var dbContextType in dbContextTypes)
            {
                EntityFrameworkGenericRepositoryRegistrar.RegisterForDbContext(dbContextType, UPrimeEngine.Instance.IocManager);
            }

            var a = 1;

            //Entity 1 (with default PK)
            //var entity1Repository = UPrimeEngine.Instance.Resolve<IRepository<MyEntity3>>();
            //entity1Repository.Equals(null);
            //(entity1Repository is EfRepositoryBase<MyMainDbContext, MyEntity1>).ShouldBe(true);

            //Entity 1 (with specified PK)
            //var entity1RepositoryWithPk = LocalIocManager.Resolve<IRepository<MyEntity1, int>>();
            //entity1RepositoryWithPk.ShouldNotBe(null);
            //(entity1RepositoryWithPk is EfRepositoryBase<MyMainDbContext, MyEntity1, int>).ShouldBe(true);

            ////Entity 2
            //var entity2Repository = LocalIocManager.Resolve<IRepository<MyEntity2, long>>();
            //(entity2Repository is EfRepositoryBase<MyMainDbContext, MyEntity2, long>).ShouldBe(true);
            //entity2Repository.ShouldNotBe(null);

            ////Entity 3
            //var entity3Repository = LocalIocManager.Resolve<IMyModuleRepository<MyEntity3, Guid>>();
            //(entity3Repository is EfRepositoryBase<MyModuleDbContext, MyEntity3, Guid>).ShouldBe(true);
            //entity3Repository.ShouldNotBe(null);
        }

        public class MyEFConfiguration : IEFConfiguration
        {

            public bool OpenedReadAndWrite
            {
                get { return false; }
            }

            public string SqlConnectionString
            {
                get { return "Server=localhost;Database=test;User=sa;Password=123"; }
            }

            public string ReadSqlConnectionString
            {
                get { return "Server=localhost;Database=test;User=sa;Password=123"; }
            }
        }
        public class MyMainDbContext : UDbContext
        {
            public virtual DbSet<MyEntity2> MyEntities2 { get; set; }

            public virtual DbSet<MyNonEntity> MyNonEntities { get; set; }
        }

        public class MyModuleDbContext : UDbContext
        {
            public virtual DbSet<MyEntity3> MyEntities3 { get; set; }
        }

        public class MyEntity1 : Entity
        {

        }

        public class MyEntity2 : Entity<long>
        {

        }

        public class MyEntity3 : Entity
        {

        }

        public class MyNonEntity
        {

        }

        public interface IMyEntity1Repository : IRepository<MyEntity1>
        {

        }

        public class MyEntity1Repository : MyModuleRepositoryBase<MyEntity1>, IMyEntity1Repository
        {
            public MyEntity1Repository(IDbContext dbContextProvider)
                : base(dbContextProvider)
            {
            }
        }

        public interface IMyModuleRepository<TEntity> : IRepository<TEntity>
            where TEntity : class, IEntity<int>
        {

        }

        public interface IMyModuleRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
            where TEntity : class, IEntity<TPrimaryKey>
        {

        }

        public class MyModuleRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<MyModuleDbContext, TEntity, TPrimaryKey>, IMyModuleRepository<TEntity, TPrimaryKey>
            where TEntity : class, IEntity<TPrimaryKey>
        {
            public MyModuleRepositoryBase(IDbContext dbContextProvider)
                : base(dbContextProvider)
            {
            }
        }

        public class MyModuleRepositoryBase<TEntity> : MyModuleRepositoryBase<TEntity, int>, IMyModuleRepository<TEntity>
            where TEntity : class, IEntity<int>
        {
            public MyModuleRepositoryBase(IDbContext dbContextProvider)
                : base(dbContextProvider)
            {
            }
        }
    }
}
