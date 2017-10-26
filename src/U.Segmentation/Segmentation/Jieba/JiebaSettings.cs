using U.Settings;

namespace U.Segmentation.Jieba
{
    /// <summary>
    /// 分词需要用到的自定义配置
    /// </summary>
    [USettingsPathArribute("JiebaSettings.json", "/Config/U/Segmentation/")]
    public class JiebaSettings : USettings<JiebaSettings>
    {
        /// <summary>
        /// 反向词频文件地址
        /// </summary>
        public string IdfFileName { get; set; }

        public string StopWordsFileName { get; set; }

        public string MainDictFileName { get; set; }

        public string ProbTransFileName { get; set; }

        public string ProbEmitFileName { get; set; }

        public string PosProbStartFileName { get; set; }

        public string PosProbTransFileName { get; set; }

        public string PosProbEmitFileName { get; set; }

        public string CharStateTabFileName { get; set; }
    }
}
