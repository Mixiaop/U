
namespace U
{
    /// <summary>
    /// 定义一些U框架的常量
    /// </summary>
    public static class UConsts
    {
        /// <summary>
        /// 当前版本
        /// </summary>
        public const string CurrentVersion = "0.5.9.5";
    }
    //0.5.4.1 修正了【AjaxProGeneratePageBase】生成ajaxservices兼容腾讯手机X5浏览器
    //0.5.4.2 CookHelper增加了重载支持存储domain
    //0.5.5.2 新增加了U.Converters【用于处理特殊的HtmlToImage、WordToHtml、HtmlToWord】等转换功能
    //0.5.5.3 【U.Converters】URL转图片使用了Seliem.WebDriver
    //0.5.6.4 应用服务增加了对【实现的事件订阅与发布功能】
    //0.5.7.4 增加了U.Utilites 对IIS7 8 域名的获取与添加
    //0.5.7.5 增加了U.Utilites 对IIS7 8 域名的删除
    //0.5.8.5 增加了U.Segmentation 分词功能（Jieba分词的.NET实现 ISegmenter）
    //        U.Utilites config路径变更 /Config/U/Utilities
    //0.5.9.5 增加了U.Utilities.Compress 压缩的命名空间 ZipHelper
}
