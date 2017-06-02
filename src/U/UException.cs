using System;
using System.Runtime.Serialization;

namespace U
{
    /// <summary>
    /// YOU 系统抛出的特定异常的基类型
    /// </summary>
    [Serializable]
    public class UException : Exception
    {
        public UException()
        {

        }

        public UException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        public UException(string message)
            : base(message)
        {

        }


        public UException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
