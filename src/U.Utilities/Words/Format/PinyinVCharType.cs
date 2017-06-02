
namespace U.Utilities.Words.Format
{
    /// <summary>
    /// 定义格式化返回的字符 'ü' 的类型
    /// 
    /// 1. WITH_U_AND_COLON -> u:
    /// 2. WITH_V           -> v
    /// 3. WITH_U_UNICODE   -> ü
    /// </summary>
    public enum PinyinVCharType
    {
        WITH_U_AND_COLON,       //  输出字符 'ü' 使用 "u:"
        WITH_V,                 //  输出字符 'ü' 使用 "v"
        WITH_U_UNICODE          //  输出字符 'ü' 使用 "ü"
    }
}
