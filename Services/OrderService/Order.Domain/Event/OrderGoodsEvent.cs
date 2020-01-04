using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Event
{
    public class OrderGoodsEvent: IEvent
    {
        public Guid GoodsId { get; set; }
        public int Count { get; set; }
    }
}
