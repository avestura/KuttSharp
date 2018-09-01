using System;
using System.Collections.Generic;
using System.Text;

namespace KuttSharp
{
    public class KuttException : Exception
    {
        public KuttException() : base()
        {
        }

        protected KuttException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public KuttException(string message) : base(message)
        {
        }

        public KuttException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
