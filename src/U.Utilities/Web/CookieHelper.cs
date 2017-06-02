using System;
using System.Web;
using System.Text;
using System.Web.Security;

namespace U.Utilities.Web
{
    public class CookieHelper
    {
        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        /// <param name="domain"></param>
        public static void ClearCookie(string cookiename, string domain = "")
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                if (domain.IsNotNullOrEmpty())
                    cookie.Domain = domain;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        /// <returns></returns>
        public static string GetCookieValue(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            string str = string.Empty;
            if (cookie != null)
            {
                //HttpContext
                str = HttpUtility.UrlDecode(cookie.Value, Encoding.GetEncoding("utf-8"));
            }
            return str;
        }
        /// <summary>
        /// 添加一个Cookie（24小时过期）
        /// </summary>
        /// <param name="cookiename"></param>
        /// <param name="cookievalue"></param>
        /// <param name="domain"></param>
        public static void SetCookie(string cookiename, string cookievalue, string domain = "")
        {
            SetCookie(cookiename, cookievalue, DateTime.Now.AddDays(1.0));
        }
        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        /// <param name="domain"></param>
        public static void SetCookie(string cookiename, string cookievalue, DateTime expires, string domain = "")
        {
            HttpCookie cookie = new HttpCookie(cookiename)
            {
                Value = HttpUtility.UrlEncode(cookievalue, Encoding.GetEncoding("utf-8")),
                Expires = expires
            };
            if (domain.IsNotNullOrEmpty())
            {
                cookie.Domain = domain;
            }

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}
