using System;
using System.Runtime.Serialization;

namespace Store.BLL.Exceptions
{
    [Serializable]
    public class ProductAlreadyExistsException: Exception
    {
        public ProductAlreadyExistsException()
        {
        }

        public ProductAlreadyExistsException(string message) : base(message)
        {
        }

        public ProductAlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }
        protected ProductAlreadyExistsException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }
    }
}
