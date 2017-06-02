using Autofac;

namespace U.Dependency
{
    /// <summary>
    /// 此类用于注册最基本的依赖实现“瞬态、单例” <see cref="ISingletonDependency"/> 和 <see cref="ITransientDependency"/>
    /// </summary>
    public class BasicConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {

            ContainerBuilder builder = new ContainerBuilder();
            //Transient
            builder.RegisterAssemblyTypes(context.Assembly)
                .As<ITransientDependency>()
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            //Singleton
            builder.RegisterAssemblyTypes(context.Assembly)
                .As<ISingletonDependency>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.Update(context.IocManager.IocContainer);
        }
    }
}
