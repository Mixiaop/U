using System;

namespace U.Events.Bus.Exceptions
{
    /// <summary>
    /// 异常事件数据 ，用于来自异常的通知
    /// This type of events can be used to notify for an exception
    /// </summary>
    public class ExceptionData : EventData
    {
        public Exception Exception { get; set; }

        public ExceptionData(Exception exception) {
            Exception = exception;
        }
    }
}
