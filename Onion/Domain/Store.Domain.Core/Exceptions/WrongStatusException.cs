using System;
using System.Runtime.Serialization;

namespace Store.Domain.Core.Exceptions
{
    /// <summary>
    /// Class WrongStatusException.
    /// Implements the <see cref="Exception" />
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class WrongStatusException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WrongStatusException"/> class.
        /// </summary>
        public WrongStatusException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WrongStatusException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public WrongStatusException(string message): base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WrongStatusException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public WrongStatusException(string message, Exception inner): base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WrongStatusException"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        protected WrongStatusException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }
    }
}
