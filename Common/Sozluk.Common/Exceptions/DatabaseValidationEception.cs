using System;
using System.Runtime.Serialization;

namespace Sozluk.Common.Exceptions
{
    public class DatabaseValidationEception : Exception
    {
        public DatabaseValidationEception()
        {
        }

        public DatabaseValidationEception(string? message) : base(message)
        {
        }

        public DatabaseValidationEception(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DatabaseValidationEception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

