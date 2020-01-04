using System;
using System.Collections.Generic;
using System.Text;

namespace DomainBase
{
    public abstract class CustomerException : Exception
    {
        public CustomerException(string message) : base(message) { }
        public int Code { get; set; }
    }
}
