using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SharePlatformSystem.Core.Exceptions
{
  /// <summary>
  /// This exception is thrown if a problem on ABP initialization progress.
  /// </summary>
    [Serializable]
    public class SharePlatformInitializationException : SharePlatformException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SharePlatformInitializationException()
        {

        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public SharePlatformInitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public SharePlatformInitializationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public SharePlatformInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
