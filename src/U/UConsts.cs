
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
        public const string CurrentVersion = "0.6.11.11";
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
    //0.5.9.6 修复U.Utilities.Security签名问题，异常会自动给当前时间
    //0.5.9.7 增加重启IIS7的站点业务
    //0.5.10.7 新增加对应主机IIS的管理服务，使用U.Utilites.IIS.UIISManageClient来做客户端的调用类
    //0.5.10.8 修复U.Utilities.Security签名不支持数字表格的BUG
    //0.5.10.9 修复IIS重启无效的BUG
    //0.5.10.10 U.Utilites.Net.WebRequestHelper 增加httppost的重载 
    //0.5.11.10 更新了标签式缓存的使用方式（CachingAttr）
    //0.5.11.11 修复了CachingInterceptor注册方式，从 ITransientDependency 修改成 IApplicationService
    //0.6.11.11 4.5.1 升级到 4.6.1
}
