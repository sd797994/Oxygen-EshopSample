using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationBase
{
    public class ApplicationException : CustomerException
    {
        public ApplicationException(string message, int code = -1) : base(message) { Code = code; }
    }
}
