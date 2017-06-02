using System;

namespace U.Web.Models
{
    /// <summary>
    /// Used to store information about a validation error.
    /// </summary>
    [Serializable]
    public class ValidationErrorInfo
    {
        /// <summary>
        /// validation errror message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Relate invalid members(fields or properties)
        /// </summary>
        public string[] Members { get; set; }

        public ValidationErrorInfo() { }

        public ValidationErrorInfo(string message)
        {
            Message = message;
        }

        public ValidationErrorInfo(string message, string[] members)
            : this(message)
        {
            Members = members;
        }

        public ValidationErrorInfo(string message, string member) : this(message, new[] { member }) { }
    }
}
