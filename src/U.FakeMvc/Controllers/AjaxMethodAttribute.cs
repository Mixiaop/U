using System;

namespace U.FakeMvc.Controllers
{
    /// <summary>
    /// 标记AJAX方法需要系统生成
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AjaxMethodAttribute : Attribute
    {
    }
}
