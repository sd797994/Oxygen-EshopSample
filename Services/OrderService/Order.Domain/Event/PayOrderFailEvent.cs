using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Event
{
    public class PayOrderFailEvent : CurrentUserEvent, IEvent
    {
        public decimal TotalPrice { get; set; }
    }
}
