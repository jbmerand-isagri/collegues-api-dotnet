using System;
using System.Runtime.Serialization;

namespace ColleguesApi.Services
{
    [Serializable]
    public class CollegueInvalideException : Exception
    {
        public CollegueInvalideException()
        {
        }

        public CollegueInvalideException(string message) : base(message)
        {
        }

        public CollegueInvalideException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CollegueInvalideException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}