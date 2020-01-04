using System;
using System.Collections.Generic;
using System.Text;

namespace DomainBase
{
    public abstract class CurrentUserEvent
    {
        public Guid UserId { get; set; }
        public string Account { get; set; }
    }
}
