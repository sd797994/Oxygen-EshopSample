using System;
using System.Collections.Generic;
using System.Text;
using Trade.Domain.Aggregation;
using Trade.Domain.Aggregation.ValueObject;

namespace Trade.Domain.Factory
{
    public class TradeRecordFactory
    {
        public TradeRecordEntity Create(Guid tradeId, decimal amount)
        {
            return new TradeRecordEntity() { Id = Guid.NewGuid(), TradeId = tradeId, Amount = amount, TradeType = amount > 0 ? TradeTypeEnum.Recharge : TradeTypeEnum.Deduction, TradeTime = DateTime.Now };
        }
    }
}
