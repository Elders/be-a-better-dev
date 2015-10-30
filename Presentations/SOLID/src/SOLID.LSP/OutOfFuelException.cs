using System;
using System.Runtime.Serialization;

namespace SOLID.LSP
{
    [Serializable]
    public class OutOfFuelException : Exception
    {
        public OutOfFuelException()
        {
        }

        public OutOfFuelException(string message) : base(message)
        {
        }

        public OutOfFuelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OutOfFuelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}