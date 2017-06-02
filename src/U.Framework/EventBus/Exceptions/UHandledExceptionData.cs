using System;

namespace U.EventBus.Exceptions
{
    /// <summary>
    /// This type of events are used to notify for exceptions handled by U infrastructure.
    /// </summary>
    public class UHandledExceptionData : ExceptionData
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exception">Exception object</param>
        public UHandledExceptionData(Exception exception)
            : base(exception)
        {

        }
    }
}
