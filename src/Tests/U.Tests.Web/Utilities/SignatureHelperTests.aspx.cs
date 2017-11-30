using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.Utilities.Security;

namespace U.Tests.Web.Utilities
{
    public partial class SignatureHelperTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var sign = new PassSignature();
            //var str = sign.Generate();
            //Response.Write(str + "<br />");

            //Response.Write(sign.Validation(str, 60));

            var str = ZeroBSignatureHelper.Generate();

            Response.Write(ZeroBSignatureHelper.Validation(str));
        }
    }

    /// <summary>
    /// 优志愿密码簿V1版
    /// 24小时制，例：2015-12-24 23:55:12
    /// 密码对照表：0 1 2 3 4 5 6 7 8 9 - : 空格 随机符
    ///             Z 0 T t F f S s E N L D B    C
    /// 组合规则：随机符为随机位次穿插
    /// 样式结果：TZOfLOCTLTFBTtDffDOT
    /// 20位
    /// </summary>
    public class ZeroBSignatureHelper
    {

        public static string Generate()
        {
            var sign = GetSign();

            return sign.Generate();
        }

        public static bool Validation(string signStr, int expiresSecond = 300)
        {
            var sign = GetSign();

            return sign.Validation(signStr, expiresSecond);
        }

        public static PassSignature GetSign()
        {
            PassSignatureTable table = new PassSignatureTable();
            table.Num0 = "Z";
            table.Num1 = "0";
            table.Num2 = "T";
            table.Num3 = "t";
            table.Num4 = "F";
            table.Num5 = "f";
            table.Num6 = "S";
            table.Num7 = "s";
            table.Num8 = "E";
            table.Num9 = "N";
            table.Str1 = "L";
            table.Str2 = "D";
            table.Space = "a";
            table.RandStr = "A";
            PassSignature sign = new PassSignature(table);

            return sign;
        }

    }
}