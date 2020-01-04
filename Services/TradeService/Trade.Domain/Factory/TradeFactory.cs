using System;
using System.Collections.Generic;
using System.Text;
using Trade.Domain.Aggregation;

namespace Trade.Domain.Factory
{
    public class TradeFactory
    {
        public TradeEntity Create(Guid userid)
        {
            return new TradeEntity() { Id = Guid.NewGuid(), UserId = userid, Balance = 0 };
        }
    }
}
