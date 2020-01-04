using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderServiceInterface.Dtos
{
    public class OrderPayReq : BaseAuthDto
    {
        public Guid OrderId { get; set; }
    }
}
