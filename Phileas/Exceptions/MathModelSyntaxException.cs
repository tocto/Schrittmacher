using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Exceptions
{
    public class MathModelSyntaxException : Exception
    {
        public MathModelSyntaxException()
        {
        }

        public MathModelSyntaxException(string message) : base(message)
        {
        }

        public MathModelSyntaxException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MathModelSyntaxException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
