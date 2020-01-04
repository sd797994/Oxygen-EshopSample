using System;
using System.Collections.Generic;
using System.Text;

namespace DomainBase
{
    public class DomainException : CustomerException
    {
        public DomainException(string message, int code = -1) : base(message) { Code = code; }
    }
}
