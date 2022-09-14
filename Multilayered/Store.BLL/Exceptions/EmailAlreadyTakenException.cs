using System;
using System.Runtime.Serialization;

namespace Store.BLL.Exceptions
{
    [Serializable]
    public class EmailAlreadyTakenException: Exception
    {
        public EmailAlreadyTakenException()
        {
        }

        public EmailAlreadyTakenException(string message) : base(message)
        {
        }

        public EmailAlreadyTakenException(string message, Exception inner) : base(message, inner)
        {
        }

        protected EmailAlreadyTakenException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
