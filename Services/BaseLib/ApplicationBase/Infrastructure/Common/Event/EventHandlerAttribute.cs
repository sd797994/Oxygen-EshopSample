using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationBase
{
    public class EventHandlerAttribute : CapSubscribeAttribute
    {
        public EventHandlerAttribute(string name)
              : base(name)
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
