using System;
using System.Runtime.Serialization;

namespace collegues_api.Services
{
    [Serializable]
    internal class CollegueNonTrouveException : Exception
    {
        public CollegueNonTrouveException()
        {
        }

        public CollegueNonTrouveException(string message) : base(message)
        {
        }

        public CollegueNonTrouveException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CollegueNonTrouveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}