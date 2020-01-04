using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradeServiceInterface.Dtos
{
    public class TradeRollbackDto : BaseAuthDto
    {
        public decimal TotalPrice { get; set; }
    }
}
