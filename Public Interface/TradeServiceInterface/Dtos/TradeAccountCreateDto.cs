using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradeServiceInterface.Dtos
{
    public class TradeAccountCreateDto: BaseAuthDto
    {
        public Guid Id { get; set; }
    }
}
