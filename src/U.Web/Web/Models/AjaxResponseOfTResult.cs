using System;

namespace U.Web.Models
{
    /// <summary>
    /// This class used to create standed responses for ajax request
    /// 此类用于创建标准通用的Ajax请求响应
    /// </summary>
    /// <typeparam name="TResult">返回的数据类型</typeparam>
    [Serializable]
    public class AjaxResponse<TResult>
    {
        /// <summary>
        /// 就否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The actual result object of ajax request
        /// 如果请求成功，返回的结果集
        /// </summary>
        public TResult Result { get; set; }

        /// <summary>
        /// Error details
        /// </summary>
        public ErrorInfo Error { get; set; }

        /// <summary>
        /// This property can be used to indicate that current user has no privilege to perform this request 
        /// </summary>
        public bool UnAuthorizedRequest { get; set; }

        public AjaxResponse(TResult result)
        {
            Result = result;
            Success = true;
        }

        public AjaxResponse()
        {
            Success = true;
        }

        public AjaxResponse(bool success)
        {
            Success = success;
        }

        public AjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
        {
            Error = error;
            UnAuthorizedRequest = unAuthorizedRequest;
            Success = false;
        }
    }
}
