using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Event
{
    public class CreateOrderEvent : CurrentUserEvent, IEvent
    {
        public CreateOrderEvent(Guid orderId)
        {
            OrderId = orderId;
        }
        public Guid OrderId { get; set; }
    }
}
