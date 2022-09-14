using System;
using System.Runtime.Serialization;

namespace Store.BLL.Exceptions
{
    [Serializable]
    public class NoOrdersException: Exception
    {
        public NoOrdersException()
        {
        }

        public NoOrdersException(string message): base(message)
        {
        }

        public NoOrdersException(string message, Exception inner): base(message, inner)
        {
        }

        protected NoOrdersException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }
    }
}
