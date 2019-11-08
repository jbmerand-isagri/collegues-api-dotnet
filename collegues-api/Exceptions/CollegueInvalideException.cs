using System;
using System.Runtime.Serialization;

namespace collegues_api.Services
{
    [Serializable]
    internal class CollegueInvalideException : Exception
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