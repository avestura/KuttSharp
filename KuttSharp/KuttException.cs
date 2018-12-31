using System;
using System.Runtime.Serialization;

namespace KuttSharp
{
    /// <summary>
    /// Exceptions that are related to Kutt Api
    /// </summary>
    public class KuttException : Exception
    {
        public KuttException(){}

        protected KuttException(SerializationInfo info,StreamingContext context) : base(info, context){}

        public KuttException(string message) : base(message){}

        public KuttException(string message, Exception innerException) : base(message, innerException){}
    }
}
