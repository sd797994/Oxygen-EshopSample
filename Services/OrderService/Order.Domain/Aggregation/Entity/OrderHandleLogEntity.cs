using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Aggregation
{
    public class OrderHandleLogEntity : AggregateRoot
    {
        public string OrderNo { get; set; }
        public Guid? OrderId { get; set; }
        public bool HandleSuccess { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string LogContent { get; set; }
    }
}
