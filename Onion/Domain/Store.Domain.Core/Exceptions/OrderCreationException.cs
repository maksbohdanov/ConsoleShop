using System;
using System.Runtime.Serialization;

namespace Store.Domain.Core.Exceptions
{
    /// <summary>
    /// Class OrderCreationException.
    /// Implements the <see cref="Exception" />
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class OrderCreationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderCreationException"/> class.
        /// </summary>
        public OrderCreationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderCreationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OrderCreationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderCreationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public OrderCreationException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderCreationException"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        protected OrderCreationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
