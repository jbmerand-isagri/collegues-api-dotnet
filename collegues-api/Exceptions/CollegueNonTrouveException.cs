﻿using System;
using System.Runtime.Serialization;

namespace ColleguesApi.Services
{
    [Serializable]
    public class CollegueNonTrouveException : Exception
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