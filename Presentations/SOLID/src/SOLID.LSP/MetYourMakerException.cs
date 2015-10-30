using System;
using System.Runtime.Serialization;

namespace SOLID.LSP
{
    [Serializable]
    public class MetYourMakerException : Exception
    {
        public MetYourMakerException()
        {
        }

        public MetYourMakerException(string message) : base(message)
        {
        }

        public MetYourMakerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MetYourMakerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}