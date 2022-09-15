using System;
using System.Runtime.Serialization;

namespace Store.Domain.Core.Exceptions
{
    /// <summary>
    /// Class NoOrdersException.
    /// Implements the <see cref="Exception" />
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class NoOrdersException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoOrdersException"/> class.
        /// </summary>
        public NoOrdersException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoOrdersException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NoOrdersException(string message): base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoOrdersException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public NoOrdersException(string message, Exception inner): base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoOrdersException"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        protected NoOrdersException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }
    }
}
