using System;

namespace U.FakeMvc.Controllers
{
    /// <summary>
    /// 标记控制器的方法为视图
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpGetAttribute : Attribute
    {
        
    }
}
