using System;

namespace U.Events.Bus.Exceptions
{
    /// <summary>
    /// This type of events are used to notify for exceptions handled by U infrastructure.
    /// </summary>
    public class UHandledExceptionData:  ExceptionData
    {
        public UHandledExceptionData(Exception exception)
            : base(exception)
        {

        }
    }
}
