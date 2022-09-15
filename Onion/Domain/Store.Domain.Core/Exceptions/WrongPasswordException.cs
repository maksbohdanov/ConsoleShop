using System;
using System.Runtime.Serialization;

namespace Store.Domain.Core.Exceptions
{
    /// <summary>
    /// Class WrongPasswordException.
    /// Implements the <see cref="Exception" />
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class WrongPasswordException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WrongPasswordException"/> class.
        /// </summary>
        public WrongPasswordException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WrongPasswordException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public WrongPasswordException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WrongPasswordException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public WrongPasswordException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WrongPasswordException"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        protected WrongPasswordException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
