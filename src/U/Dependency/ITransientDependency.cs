
namespace U.Dependency
{
    /// <summary>
    /// 【瞬时】，每次请求都会创建对象，所有实现此接口的类会被自动注册到依赖注入
    /// </summary>
    public interface ITransientDependency
    {
    }
}
