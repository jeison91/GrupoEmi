using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() { }
        public BadRequestException(string Message) : base(Message) { }
        public BadRequestException(string Message, Exception inner) : base(Message, inner) { }
    }
}
