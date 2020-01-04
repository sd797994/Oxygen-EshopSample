using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trade.Domain.Aggregation;
using Trade.Domain.Repository;

namespace Trade.Domain.Specification
{
    public class TradeCreateSpecification : IOperateSpecification<TradeEntity>
    {
        private readonly ITradeRepository tradeRepository;
        public TradeCreateSpecification(ITradeRepository tradeRepository)
        {
            this.tradeRepository = tradeRepository;
        }
        public async Task<bool> SatisfiedBy(TradeEntity entity)
        {
            if (await tradeRepository.CountAsync(x => x.UserId == entity.UserId) > 0)
            {
                throw new DomainException("创建交易不能重复,请重新添加!");
            }
            return true;
        }
    }
}
