using SharePlatformSystem.Core.Exceptions;
using System;
using System.Runtime.Serialization;

namespace SharePlatformSystem.Domain.Uow
{
    [Serializable]
    public class SharePlatformDbConcurrencyException : SharePlatformException
    {
        /// <summary>
        /// Creates a new <see cref="SharePlatformDbConcurrencyException"/> object.
        /// </summary>
        public SharePlatformDbConcurrencyException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="SharePlatformException"/> object.
        /// </summary>
        public SharePlatformDbConcurrencyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="SharePlatformDbConcurrencyException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public SharePlatformDbConcurrencyException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="SharePlatformDbConcurrencyException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public SharePlatformDbConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}