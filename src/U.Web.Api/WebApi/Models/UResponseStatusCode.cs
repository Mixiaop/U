using U.CodeAnnotations;

namespace U.WebApi.Models
{
    /// <summary>
    /// 响应状态码
    /// </summary>
    public enum UResponseStatusCode
    {
        //************************
        //*      系统级          *
        //************************
        /// <summary>
        /// 请求参数有误
        /// </summary>
        [EnumAlias("请求参数有误")]
        RequestParameterIsWrong = -1,
        /// <summary>
        /// 错误消息返回
        /// </summary>
        [EnumAlias("错误消息返回")]
        Error = 0,
        /// <summary>
        /// 成功返回
        /// </summary>
        [EnumAlias("成功返回")]
        Ok = 1,
        /// <summary>
        /// 发生了一般错误
        /// </summary>
        [EnumAlias("发生了一般错误")]
        InternalServerError = 100,
        /// <summary>
        /// 系统已关闭
        /// </summary>
        [EnumAlias("系统已关闭")]
        SystemClose = 200,
        /// <summary>
        /// 签名失败
        /// </summary>
        [EnumAlias("签名失败")]
        SignatureFailed = 300,
        /// <summary>
        /// 身份授权失败
        /// </summary>
        [EnumAlias("身份授权失败")]
        Unauthorized = 401
    }
}
