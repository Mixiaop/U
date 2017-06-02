using System;
using System.Runtime.Serialization;
using U.Web.Models;

namespace U.WebApi.Client
{
    /// <summary>
    /// 当访问接口应用的方法时抛出自异常
    /// </summary>
    [Serializable]
    public class URemoteCallException: UException
    {
        /// <summary>
        /// 远程错误信息
        /// </summary>
        public ErrorInfo ErrorInfo { get; set; }

        public URemoteCallException() { }

        public URemoteCallException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context) { }

        public URemoteCallException(ErrorInfo errorInfo) : base(errorInfo.Message) {
            ErrorInfo = errorInfo;
        }

        public URemoteCallException(string errorMessage) : base(errorMessage) {
            ErrorInfo = new ErrorInfo(errorMessage);
        }
    }
}
