using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace TestOkur.Configuration
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class ConfigurationValidationException : Exception
    {
        public ConfigurationValidationException()
        {
        }

        public ConfigurationValidationException(string message)
            : base(message)
        {
        }

        public ConfigurationValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ConfigurationValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}