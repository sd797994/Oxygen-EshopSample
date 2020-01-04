using ApplicationBase;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Trade.Domain.Factory;
using Trade.Domain.Repository;
using Trade.Domain.Specification;
using TradeServiceInterface.Dtos;
using ApplicationException = ApplicationBase.ApplicationException;

namespace TradeServiceInterface.EventHandler
{
    public class TradeAccountCreate : BaseEventHandler<TradeAccountCreateDto>, IEventHandler
    {
        private readonly ITradeRepository tradeRepository;
        public TradeAccountCreate(ITradeRepository tradeRepository, IIocContainer iocContainer) : base(iocContainer)
        {
            this.tradeRepository = tradeRepository;
        }
        [EventHandler("EshopSample.UserCreateEvent")]
        public override async Task Handle(TradeAccountCreateDto input)
        {
            await HandleAsync(input, async () =>
            {
                //通过工厂创建交易领域对象
                var trade = new TradeFactory().Create(input.Id);
                //规约校验
                if (await new TradeCreateSpecification(tradeRepository).SatisfiedBy(trade))
                {
                    //仓储持久化
                    tradeRepository.Add(trade);
                    await tradeRepository.SaveAsync();
                }
            });
        }
    }
}
