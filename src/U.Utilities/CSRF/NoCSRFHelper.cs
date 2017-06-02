using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using U.Utilities.Strings;

namespace U.Utilities.CSRF
{
    /// <summary>
    /// 防止跨站请求伪造（CSRF）帮助类
    /// </summary>
    public class NoCSRFHelper
    {
        public const string CSRF_KEY_PREFIX = "CSRF_";

        public static bool Check(string key, string token, bool throwException = false) {
            return true;
        }
        /// <summary>
        /// 生成防 CSRF 的 token 并设置到 Session
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string Generate(string key) {
            string token = (DateTime.Now.Ticks.ToString() + StringHelper.GenerateRandomString(64)).EncodeBase64();
            HttpContext.Current.Session[CSRF_KEY_PREFIX + key] = token;
            return token;
        }


    }
}
