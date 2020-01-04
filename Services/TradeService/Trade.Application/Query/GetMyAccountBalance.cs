using ApplicationBase;
using DomainBase;
using Microsoft.Extensions.Logging;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trade.Domain.Aggregation;
using Trade.Domain.Repository;
using TradeServiceInterface;
using TradeServiceInterface.Dtos;
using TradeServiceInterface.Query;
using ApplicationException = ApplicationBase.ApplicationException;
using System.Linq;
using Trade.Domain.Aggregation.ValueObject;

namespace Trade.Application.Query
{
    public class GetMyAccountBalance : BaseQueryService<MyAccountBalanceDto, MyAccountResponse>, IGetMyAccountBalance
    {
        private readonly ITradeRepository tradeRepository;
        private readonly ITradeRecordRepository recordRepository;
        public GetMyAccountBalance(ITradeRepository tradeRepository, ITradeRecordRepository recordRepository, IIocContainer iocContainer) : base(iocContainer)
        {
            this.tradeRepository = tradeRepository;
            this.recordRepository = recordRepository;
        }
        public override async Task<BaseApiResult<MyAccountResponse>> Query(MyAccountBalanceDto input)
        {
            return await HandleAsync(input, async () =>
            {
                var trade = await tradeRepository.GetAsync(x => x.UserId == input.UserId);
                if (trade == null)
                {
                    throw new ApplicationException("没有找到账户资金信息!");
                }
                var result = new MyAccountResponse()
                {
                    Balance = trade?.Balance ?? 0,
                    Records = (await recordRepository.GetManyAsync(x => x.TradeId == trade.Id)).OrderByDescending(x=>x.TradeTime).Select(x => new BalanceRecord() { Content = $"{(x.TradeType==TradeTypeEnum.Recharge?"充值":"扣款")}{x.Amount}元", Time = x.TradeTime }).ToList()
                };
                return result;
            });
        }
    }
}
