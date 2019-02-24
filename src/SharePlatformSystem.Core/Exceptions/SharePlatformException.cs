using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SharePlatformSystem.Core.Exceptions
{
   /// <summary>
   /// Base exception type for those are thrown by Abp system for Abp specific exceptions.
   /// </summary>
    [Serializable]
    public class SharePlatformException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="SharePlatformException"/> object.
        /// </summary>
        public SharePlatformException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="SharePlatformException"/> object.
        /// </summary>
        public SharePlatformException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="SharePlatformException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public SharePlatformException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="SharePlatformException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public SharePlatformException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
