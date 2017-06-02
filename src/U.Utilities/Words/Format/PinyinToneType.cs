
namespace U.Utilities.Words.Format
{
    /// <summary>
    /// 定义返回的格式化拼音最后一位是否带音调
    /// 
    /// 例：
    /// 
    /// 1、WITH_TONE_NUMBER  -> da3
    /// 2、WITHOUT_TONE      -> da
    /// 3、WITH_TONE_MARK    -> dǎ
    /// </summary>
    public enum PinyinToneType
    {
        WITH_TONE_NUMBER,   
        WITHOUT_TONE,       
        WITH_TONE_MARK     
    }
}
