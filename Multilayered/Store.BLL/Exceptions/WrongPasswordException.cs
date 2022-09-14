using System;
using System.Runtime.Serialization;

namespace Store.BLL.Exceptions
{
    [Serializable]
    public class WrongPasswordException: Exception
    {
        public WrongPasswordException()
        {
        }

        public WrongPasswordException(string message) : base(message)
        {
        }

        public WrongPasswordException(string message, Exception inner) : base(message, inner)
        {
        }

        protected WrongPasswordException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
