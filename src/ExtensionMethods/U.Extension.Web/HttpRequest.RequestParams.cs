using System.Collections.Generic;
using System.Linq;
using System.Web;
public static partial class Extensions
{
    public static List<string> RequestParams(this HttpRequest Request)
    {
        List<string> source = new List<string>();
        source.AddRange(Request.Form.AllKeys);
        source.AddRange(Request.QueryString.AllKeys);
        return source.Distinct<string>().ToList<string>();
    }

    public static List<string> RequestParams(this HttpRequestBase Request)
    {
        List<string> source = new List<string>();
        source.AddRange(Request.Form.AllKeys);
        source.AddRange(Request.QueryString.AllKeys);
        return source.Distinct<string>().ToList<string>();
    }
}