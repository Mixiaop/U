
namespace U.Dependency
{
    /// <summary>
    /// 此接口用于注册约定的依赖（BasicConventionalRegistrar）（这里指<see cref="ISingletonDependency"/> and <see cref="ITransientDependency"/>
    /// </summary>
    public interface IConventionalDependencyRegistrar
    {
        /// <summary>
        /// registers types of given assembly of convention.
        /// </summary>
        /// <param name="context"></param>
        void RegisterAssembly(IConventionalRegistrationContext context);
    }
}
