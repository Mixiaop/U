using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using U;
namespace U.Runtime.Caching
{
    public class CachingHelper
    {
        public static bool HasCachingAttribute(MemberInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(CachingAttribute), true);
        }
    }
}