using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderServiceInterface.Dtos
{
    public class QueryOrderReq : OrderQueryOuthReq
    {
        public Guid? OrderId { get; set; }
        public int? OrderType { get; set; }
    }
}
