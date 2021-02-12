using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Exceptions
{
    public class IncompleteMathModelException : Exception
    {
        public IncompleteMathModelException()
        {
        }

        public IncompleteMathModelException(string message) : base(message)
        {
        }

        public IncompleteMathModelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IncompleteMathModelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
