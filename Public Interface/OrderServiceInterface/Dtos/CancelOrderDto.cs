using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderServiceInterface.Dtos
{
    public class CancelOrderDto : BaseAuthDto
    {
        public Guid OrderId { get; set; }
    }
}
