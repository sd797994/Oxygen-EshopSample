using ApplicationBase;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trade.Domain.Factory;
using Trade.Domain.Repository;
using TradeServiceInterface.Dtos;
using ApplicationException = ApplicationBase.ApplicationException;

namespace Trade.Application.EventHandler
{
    public class TradeRollback : BaseEventHandler<TradeRollbackDto>, IEventHandler
    {
        private readonly ITradeRepository tradeRepository;
        private readonly ITradeRecordRepository tradeRecordRepository;
        private readonly ITransaction transaction;
        public TradeRollback(ITradeRepository tradeRepository, ITradeRecordRepository tradeRecordRepository, ITransaction transaction, IIocContainer iocContainer) : base(iocContainer)
        {
            this.tradeRepository = tradeRepository;
            this.tradeRecordRepository = tradeRecordRepository;
            this.transaction = transaction;
        }
        [EventHandler("EshopSample.PayOrderFailEvent")]
        public override async Task Handle(TradeRollbackDto input)
        {
            await HandleAsync(input, async () =>
            {
                //获取交易对象
                var trade = await tradeRepository.GetAsync(x => x.UserId == input.UserId);
                if (trade == null)
                {
                    throw new ApplicationException("没有找到账户交易信息!");
                }
                //执行款项变更
                trade.OperationBalance(input.TotalPrice);
                //添加交易流水
                var record = new TradeRecordFactory().Create(trade.Id, input.TotalPrice);
                using (var tran = transaction.BeginTransaction())
                {
                    //仓储添加
                    tradeRepository.Update(trade);
                    tradeRecordRepository.Add(record);
                    //工作单元持久化
                    await tradeRepository.SaveAsync();
                    tran.Commit();
                }
            });
        }
    }
}
