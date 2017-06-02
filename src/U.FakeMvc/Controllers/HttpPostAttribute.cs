using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U.FakeMvc.Controllers
{
    /// <summary>
    /// 标记控制器的方法为 Action，一般用于Insert、Update、Delete
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpPostAttribute : Attribute
    {

    }
}
