using System;

namespace SharePlatformSystem.Events.Bus.Exceptions
{
    /// <summary>
    /// This type of events are used to notify for exceptions handled by ABP infrastructure.
    /// </summary>
    public class SharePlatformHandledExceptionData : ExceptionData
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exception">Exception object</param>
        public SharePlatformHandledExceptionData(Exception exception)
            : base(exception)
        {

        }
    }
}