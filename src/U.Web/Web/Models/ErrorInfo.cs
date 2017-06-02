using System;

namespace U.Web.Models
{
    /// <summary>
    /// Use to store information about an error
    /// </summary>
     [Serializable]
    public class ErrorInfo
    {
        /// <summary>
        /// Error code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        /// 
        public string Message { get; set; }

        /// <summary>
        /// Error details
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Validation errors of exists
        /// </summary>
        public ValidationErrorInfo[] ValidationErrors { get; set; }

        public ErrorInfo()
        {

        }

        public ErrorInfo(string message)
        {
            Message = message;
        }

        public ErrorInfo(int code)
        {
            Code = code;
        }

        public ErrorInfo(int code, string message)
            : this(message)
        {
            Code = code;
        }

        public ErrorInfo(string message, string details)
            : this(message)
        {
            Details = details;
        }

        public ErrorInfo(int code, string message, string details)
            : this(message, details)
        {
            Code = code;
        }
    }
}
