
namespace U.Utilities.Words.Format
{
    /// <summary>
    /// 定义拼音的输出格式化类型
    /// 如：默认 = 大写不带单调
    /// </summary>
    public class PinyinOutputFormat
    {
        private PinyinVCharType _vcharType = PinyinVCharType.WITH_V;
        private PinyinCaseType _caseType = PinyinCaseType.LOWERCASE;
        private PinyinToneType _toneType = PinyinToneType.WITHOUT_TONE;
        
        public PinyinVCharType VCharType
        {
            get { return _vcharType; }
            set { _vcharType = value; }
        }

        public PinyinCaseType CaseType
        {
            get { return _caseType; }
            set { _caseType = value; }
        }

        public PinyinToneType ToneType
        {
            get { return _toneType; }
            set { _toneType = value; }
        }

        public PinyinOutputFormat() { }
    }
}
