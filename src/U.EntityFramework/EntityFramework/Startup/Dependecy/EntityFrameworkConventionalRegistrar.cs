using Autofac;
using U.Dependency.AutofacUtils.DynamicProxy2;
using U.Dependency;
using U.EntityFramework.Startup.Configuration;

namespace U.EntityFramework.Startup.Dependecy
{
    /// <summary>
    /// 注册来自 UDbContext 的配置
    /// </summary>
    public class EntityFrameworkConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            ContainerBuilder builder = new ContainerBuilder();

            //两个仓储之间会创建新的 DbContext
            //注意：两个仓储之间的同步操作可能会报异常。（使用仓储时，重设DbContext属性可解决此问题）
            //Transient
            builder.RegisterAssemblyTypes(context.Assembly)
                .As<UDbContext>()
                .AsSelf()
                .WithParameter("nameOrConnectionString", GetNameOrConnectionString(context.IocManager))
                .InstancePerLifetimeScope();

            builder.Update(context.IocManager.IocContainer);
        }

        private static string GetNameOrConnectionString(IIocResolver iocResolver)
        {
            var defaultConnectionString = "";
            if (iocResolver.IsRegistered<IEFConfiguration>())
            {
                defaultConnectionString = iocResolver.Resolve<IEFConfiguration>().SqlConnectionString;
            }
            return defaultConnectionString;
        }
    }
}
