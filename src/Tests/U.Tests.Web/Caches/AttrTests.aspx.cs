using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Text;
using U.Reflection;

namespace U.Tests.Web.Caches
{
    public partial class AttrTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userService = new UserGeneralCacheService();
            //Response.Write(typeof(UserCacheService).Name);
            DateTime startDate = DateTime.Now;
            bool isShow = false;
            for (int i = 0; i < 10000; i++)
            {
                var value = userService.GetUserById(1);
                if (!isShow)
                {
                    Response.Write(value);
                    isShow = true;
                }
            }
            Response.Write("<br />" + (DateTime.Now - startDate).Milliseconds);
            Response.Write("-----------------------------------<br />");
        }
    }

    public class UserGeneralCacheService
    {
        public string GetUserById(int userId)
        {
            var key = GetCurrentKey("GetUserById", 1,null,2);

            return key;
        }

        public static string GetCurrentKey(string key, params object[] args)
        {
            string localizedKey = key;
            if (args != null && args.Count() > 0)
            {
                localizedKey += "[";
                int i = 0;
                foreach (var arg in args)
                {
                    localizedKey += (arg != null ? arg.ToString() : "" )+ ",";
                    i++;
                }
                localizedKey = localizedKey.TrimEnd(',') + "]";
            }
            return localizedKey;
        }
    }

    public class UserCacheService
    {

        [CacheKey("GetUserById", "{0}", "获取单条用户信息")]
        public string GetUserById(int userId)
        {
            var key = GetCurrentKey(MethodInfo.GetCurrentMethod());

            return "GetUserById";
            return key.GetLocalizedKey(1);
        }

        public static CacheKeyAttribute GetCurrentKey(MethodBase methodInfo)
        {
            return null;
            //return methodInfo.GetCustomAttribute(typeof(CacheKeyAttribute)) as CacheKeyAttribute;
        }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }

    public class CacheServiceAttribute : Attribute
    {

    }


    [AttributeUsage(AttributeTargets.Method)]
    public class CacheKeyAttribute : Attribute
    {
        public string Name { get; private set; }

        public string Format { get; private set; }

        public string Description { get; private set; }



        public CacheKeyAttribute(string name, string format, string description)
        {
            Name = name;
            Format = format;
            Description = description;
        }

        public string GetLocalizedKey(params object[] args)
        {
            //int existsCharCount = Format.Length - Format.Replace("}", "").Length;
            //if (args.Count() != existsCharCount)
            //    throw new UException("args length worng, please check name format length");

            string localizedKey = Name + "[" + Format;
            if (args != null)
            {
                int i = 0;
                foreach (var arg in args)
                {
                    localizedKey = localizedKey.Replace("{" + i + "}", arg != null ? arg.ToString() : "");
                    i++;
                }
            }
            localizedKey += "]";
            return localizedKey;
        }
    }
}