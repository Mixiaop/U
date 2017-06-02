using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U;
using U.Domain.Repositories;
using U.EntityFramework;
using Autofac.Builder;
using Autofac;
using U.Dependency.AutofacUtils.DynamicProxy2;
using U.Dependency;
using U.EntityFramework.Startup.Configuration;
namespace U.Tests.Web.EntityFramework
{
    public partial class RepositoryTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //IDbContextProvider<MyContext> dbContextProvider = UPrimeEngine.Instance.Resolve<IDbContextProvider<MyContext>>();
            //Area area = new Area();
            //area.AreaName = "尼玛2";
            //dbContextProvider.DbContext.Areas.Add(area);
            //dbContextProvider.DbContext.SaveChanges();




            //var list = areaRepository.GetAll();
            //Response.Write("success");

            //AreaRepository areaRepository = new AreaRepository(dbContext);

            //var dbContext = new MyContext("Data Source=120.132.57.7,1444;Initial Catalog=Tests;Persist Security Info=True;User ID=sa;Password=youzy.cn");
            //Area area = new Area();
            //area.AreaName = "dbContext1";
            //dbContext.Areas.Add(area);
            //dbContext.SaveChanges();


            //var builder = new ContainerBuilder();
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //   .As<IDbContext>()
            //   .AsImplementedInterfaces()
            //   .AsSelf()
            //   .WithParameter("nameOrConnectionString", "")
            //   .InstancePerLifetimeScope();

            //var container = builder.Build();

            //var dbContext2 = container.Resolve<MyContext>();
            //Area area2 = new Area();
            //area2.AreaName = "dbContext2";
            //dbContext2.Areas.Add(area2);
            //dbContext2.SaveChanges();

            //var dbContext3 = UPrimeEngine.Instance.Resolve<MyContext>();
            //Area area3 = new Area();
            //area3.AreaName = "dbContext3";
            //dbContext3.Areas.Add(area3);
            //dbContext3.SaveChanges();

            //
            //var area = areaRepository3.Get(35);
            //areaRepository3.Delete(area);
            //areaRepository3.Insert(area3);
            //MyModuleRepositoryBase<Area> areaRepository = (MyModuleRepositoryBase<Area>)(UPrimeEngine.Instance.Resolve<IAreaRepository>());
            //MyModuleRepositoryBase<Animal> animalRepository = (MyModuleRepositoryBase<Animal>)(UPrimeEngine.Instance.Resolve<IAnimalRepository>());
            //areaRepository.Context = animalRepository.Context;

            
            //var query = from a in areaRepository.GetAll().DefaultIfEmpty()
            //            join aa in animalRepository.GetAll() on a.Id equals aa.Id into a_aa
            //            where a.Id == 1
            //            orderby a.Id descending
            //            select a;

            //var list = query.ToList();

            //foreach (var info in list) {
            //    Response.Write(info.Id);
            //}

            
            //var list = animalRepository.GetAll().ToList();

            IAreaRepository areaRepository = UPrimeEngine.Instance.Resolve<IAreaRepository>();
            var area = areaRepository.Get(4);
            area.AreaName = DateTime.Now.ToString();
            areaRepository.Update(area);

            area = areaRepository.Get(4);
            Response.Write(area.AreaName);
        }
    }

    public interface ITestService
    {
        void Show();
    }

    public class TestService : ITestService, U.Dependency.ITransientDependency
    {
        public void Show()
        {

        }
    }

    public class Test2Service : U.Dependency.ITransientDependency
    {
        public Test2Service(ITestService testService)
        {

        }
    }
}