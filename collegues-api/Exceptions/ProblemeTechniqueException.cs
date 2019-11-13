using System;
using System.Runtime.Serialization;

namespace collegues_api.Repositories
{
    [Serializable]
    internal class ProblemeTechniqueException : Exception
    {
        public ProblemeTechniqueException()
        {
        }

        public ProblemeTechniqueException(string message) : base(message)
        {
        }

        public ProblemeTechniqueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProblemeTechniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}