using U.Dependency;

namespace U.Domain.Repositories
{
    /// <summary>
    /// 仓储的接口契约，所有“仓储类”必须实现此接口
    /// 此接口为仓储接口泛型之一
    /// </summary>
    public interface IRepository : ITransientDependency
    {
        
    }
}