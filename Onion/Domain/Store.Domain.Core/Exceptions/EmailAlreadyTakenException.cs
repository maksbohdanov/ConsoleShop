using System;
using System.Runtime.Serialization;

namespace Store.Domain.Core.Exceptions
{
    /// <summary>
    /// Class EmailAlreadyTakenException.
    /// Implements the <see cref="Exception" />
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class EmailAlreadyTakenException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAlreadyTakenException"/> class.
        /// </summary>
        public EmailAlreadyTakenException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAlreadyTakenException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EmailAlreadyTakenException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAlreadyTakenException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public EmailAlreadyTakenException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAlreadyTakenException"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        protected EmailAlreadyTakenException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
