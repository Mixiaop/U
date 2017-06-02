using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace U.Tests.Web.Dependency
{
    public interface IWorkContext
    {
        string CurrentProvince { get; set; }
    }

    public class WebWorkContext : IWorkContext, U.Dependency.ITransientDependency
    {

        private const string YOUZY_CURRENT_VERSION_COOKIE_NAME = "AutofacTests.CurrentProvince";

        private string _cacheCurrentProvince = "";


        public string CurrentProvince
        {
            get
            {
                if (_cacheCurrentProvince.IsNotNullOrEmpty())
                    return _cacheCurrentProvince;

                var version = GetCurrentProvince();
                if (version.IsNotNullOrEmpty())
                {
                    return version;
                }
                else
                {
                    version = "default";
                    _cacheCurrentProvince = version;
                    SetCurrentProvince(version);

                    return _cacheCurrentProvince;
                }
            }

            set
            {
                SetCurrentProvince(value);
                _cacheCurrentProvince = value;
            }
        }


        protected string GetCurrentProvince()
        {
            if (HttpContext.Current == null || HttpContext.Current.Request == null)
                return null;

            HttpCookie cookie = HttpContext.Current.Request.Cookies[YOUZY_CURRENT_VERSION_COOKIE_NAME];

            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                var value = HttpUtility.UrlDecode(cookie.Value);
                return value;
            }
            else
                return null;
        }

        protected void SetCurrentProvince(string province)
        {
            if (HttpContext.Current != null && HttpContext.Current.Response != null)
            {
                ClearCookies(YOUZY_CURRENT_VERSION_COOKIE_NAME);

                var cookie = new HttpCookie(YOUZY_CURRENT_VERSION_COOKIE_NAME);
                cookie.HttpOnly = true;
                cookie.Value = HttpUtility.UrlEncode(province);

                int cookieExpires = 24 * 365;
                cookie.Expires = DateTime.Now.AddHours(cookieExpires);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        void ClearCookies(string cookieName)
        {
            int limit = HttpContext.Current.Request.Cookies.Count;
            int count = 0;
            for (int i = 0; i < limit; i++)
            {
                string name = HttpContext.Current.Request.Cookies[i].Name;
                if (name == cookieName)
                {
                    HttpCookie cookie = new HttpCookie(name);
                    cookie.Expires = DateTime.Now.AddMonths(-1);

                    HttpContext.Current.Response.Cookies.Add(cookie);

                    //兼容老的www.youzy.cn的cookie
                    HttpCookie cookie2 = new HttpCookie(name);
                    cookie2.Expires = DateTime.Now.AddMonths(-1);
                    cookie2.Domain = "www.youzy.cn";
                    HttpContext.Current.Response.Cookies.Add(cookie2);
                    count++;
                }
            }
        }
    }
}