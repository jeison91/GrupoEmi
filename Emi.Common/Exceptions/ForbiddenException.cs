using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Common.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() { }
        public ForbiddenException(string Message) : base(Message) { }
        public ForbiddenException(string Message, Exception inner) : base(Message, inner) { }
    }
}
