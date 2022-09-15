using System;
using System.Runtime.Serialization;

namespace Store.Domain.Core.Exceptions
{
    /// <summary>
    /// Class ProductAlreadyExistsException.
    /// Implements the <see cref="Exception" />
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class ProductAlreadyExistsException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductAlreadyExistsException"/> class.
        /// </summary>
        public ProductAlreadyExistsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ProductAlreadyExistsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public ProductAlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        protected ProductAlreadyExistsException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }
    }
}
