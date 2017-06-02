using System.Linq;
using System.Web;
public static partial class Extensions
{
    public static object RequestParamValue(this HttpRequest Request, string key)
    {
        if (Request.Form.AllKeys.Contains<string>(key))
        {
            return Request.Form[key];
        }
        if (Request.QueryString.AllKeys.Contains<string>(key))
        {
            return Request.QueryString[key];
        }
        return null;
    }

    public static object RequestParamValue(this HttpRequestBase Request, string key)
    {
        if (Request.Form.AllKeys.Contains<string>(key))
        {
            return Request.Form[key];
        }
        if (Request.QueryString.AllKeys.Contains<string>(key))
        {
            return Request.QueryString[key];
        }
        return null;
    }
}