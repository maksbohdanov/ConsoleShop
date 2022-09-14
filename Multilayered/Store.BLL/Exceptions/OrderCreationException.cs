using System;
using System.Runtime.Serialization;
namespace Store.BLL.Exceptions
{
    [Serializable]
    public class OrderCreationException : Exception
    {
        public OrderCreationException()
        {
        }

        public OrderCreationException(string message) : base(message)
        {
        }

        public OrderCreationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected OrderCreationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
