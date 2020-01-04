using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobRunner.EventHandler.Order.Dto
{
    public class TimeOutCancelOrderDto: BaseAuthDto
    {
        public Guid OrderId { get; set; }
    }
}
