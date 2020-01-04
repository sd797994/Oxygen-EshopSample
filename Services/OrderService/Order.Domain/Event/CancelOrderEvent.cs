using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Event
{
    public class CancelOrderEvent : CurrentUserEvent, IEvent
    {
        public CancelOrderEvent(Guid? orderId, List<OrderGoodsEvent> goodslist)
        {
            OrderId = orderId;
            Goodslist = goodslist;
        }
        public Guid? OrderId { get; set; }
        public List<OrderGoodsEvent> Goodslist { get; set; }
    }
}
