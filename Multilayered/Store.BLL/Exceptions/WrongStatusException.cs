using System;
using System.Runtime.Serialization;

namespace Store.BLL.Exceptions
{
    [Serializable]
    public class WrongStatusException: Exception
    {
        public WrongStatusException()
        {
        }

        public WrongStatusException(string message): base(message)
        {
        }

        public WrongStatusException(string message, Exception inner): base(message, inner)
        {
        }

        protected WrongStatusException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }
    }
}
