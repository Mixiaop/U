using System;

namespace U.Utilities.Security
{
    /// <summary>
    /// 签名加密规则
    /// 以下为例子
    /// 24小时制，例：2015-12-24 23:55:12
    /// 密码对照表：0 1 2 3 4 5 6 7 8 9 - : 空格 随机符
    ///             Z 0 T t F f S s E N L D B    C
    /// 组合规则：随机符为随机位次穿插
    /// 样式结果：TZOfLOCTLTFBTtDffDOT
    /// 20位
    /// </summary>
    public class PassSignature
    {
        PassSignatureTable _table;
        public PassSignature() {
            _table = new PassSignatureTable();
        }

        public PassSignature(PassSignatureTable table) {
            _table = table;
        }

        public string Generate()
        {
            string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            str = str.Replace("0", _table.Num0);
            str = str.Replace("1", _table.Num1);
            str = str.Replace("2", _table.Num2);
            str = str.Replace("3", _table.Num3);
            str = str.Replace("4", _table.Num4);
            str = str.Replace("5", _table.Num5);
            str = str.Replace("6", _table.Num6);
            str = str.Replace("7", _table.Num7);
            str = str.Replace("8", _table.Num8);
            str = str.Replace("9", _table.Num9);
            str = str.Replace("-", _table.Str1);
            str = str.Replace(":", _table.Str2);
            str = str.Replace(" ", _table.Space);

            var rand = new Random();
            var randNum = rand.Next(20);

            string signStr = "";
            var index = 0;
            foreach (var s in str) {
                if (index == randNum) {
                    signStr += _table.RandStr;
                }
                signStr += s.ToString();
                index++;
            }

            return signStr;

        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="sign">签名Key</param>
        /// <param name="expiresSecond">过期时间（秒）</param>
        /// <returns></returns>
        public bool Validation(string sign, int expiresSecond)
        {
            
            var result = false;
            sign = sign.Replace(_table.RandStr, "");
            if (sign.Length == 19)
            {
                var signTimeString = sign.Replace(_table.Num0, "0")
                                         .Replace(_table.Num1, "1")
                                         .Replace(_table.Num2, "2")
                                         .Replace(_table.Num3, "3")
                                         .Replace(_table.Num4, "4")
                                         .Replace(_table.Num5, "5")
                                         .Replace(_table.Num6, "6")
                                         .Replace(_table.Num7, "7")
                                         .Replace(_table.Num8, "8")
                                         .Replace(_table.Num9, "9")
                                         .Replace(_table.Str1, "-")
                                         .Replace(_table.Str2, ":")
                                         .Replace(_table.Space, " ");
                try
                {
                    DateTime now = DateTime.Now;
                    DateTime signTime = DateTime.Parse(signTimeString);
                    TimeSpan span = now - signTime;

                    //是否过期
                    if (span.TotalSeconds <= expiresSecond)
                    {
                        result = true;
                    }
                }
                catch
                {

                }
            }

            return result;
        }
    }

    #region 签名对照表
    /// <summary>
    /// 签名字符（必须为英文）
    /// 如密码对照表：0 1 2 3 4 5 6 7 8 9 - : 空格 随机符
    ///               Z 0 T t F f S s E N L D B    C
    /// </summary>
    public class PassSignatureTable
    {
        public PassSignatureTable()
        {
            Num0 = "Z";
            Num1 = "0";
            Num2 = "T";
            Num3 = "t";
            Num4 = "F";
            Num5 = "f";
            Num6 = "S";
            Num7 = "s";
            Num8 = "E";
            Num9 = "N";
            Str1 = "L";
            Str2 = "D";
            Space = "B";
            RandStr = "C";
        }

        public string Num0 { get; set; }
        public string Num1 { get; set; }
        public string Num2 { get; set; }
        public string Num3 { get; set; }
        public string Num4 { get; set; }
        public string Num5 { get; set; }
        public string Num6 { get; set; }
        public string Num7 { get; set; }
        public string Num8 { get; set; }

        public string Num9 { get; set; }

        /// <summary>
        /// 对应横杠“- ”
        /// </summary>
        public string Str1 { get; set; }

        /// <summary>
        /// 对应冒号“:”
        /// </summary>
        public string Str2 { get; set; }

        /// <summary>
        /// 空格
        /// </summary>
        public string Space { get; set; }

        /// <summary>
        /// 随机字符 
        /// </summary>
        public string RandStr { get; set; }
    }
    #endregion
}
