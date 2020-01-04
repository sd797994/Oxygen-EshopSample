using System;
using System.Collections.Generic;
using System.Text;
using DomainBase;

namespace InfrastructureBase
{
    public class InfrastructureException : CustomerException
    {
        public InfrastructureException(string message, int code = -1) : base(message) { Code = code; }
    }
}
