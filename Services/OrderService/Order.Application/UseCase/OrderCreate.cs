using ApplicationBase;
using Order.Domain.Repository;
using OrderServiceInterface.Dtos;
using OrderServiceInterface.UseCase;
using BaseServcieInterface;
using System.Threading.Tasks;
using Order.Domain.Factory;
using Order.Domain.Aggregation;
using Order.Domain.Event;
using ApplicationBase.Infrastructure.Common;

namespace Order.Application.UseCase
{
    public class OrderCreate : BaseUseCase<OrderCreateReq>, IOrderCreate
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderHandleLogRepository orderHandleLogRepository;
        private readonly ITransaction transaction;
        private readonly IEventBus eventBus;
        public OrderCreate(IOrderRepository orderRepository, IOrderHandleLogRepository orderHandleLogRepository, ITransaction transaction,
            IEventBus eventBus, IIocContainer iocContainer) : base(iocContainer)
        {
            this.orderRepository = orderRepository;
            this.orderHandleLogRepository = orderHandleLogRepository;
            this.transaction = transaction;
            this.eventBus = eventBus;
        }
        public async Task<BaseApiResult<bool>> Excute(OrderCreateReq input)
        {
            return await HandleAsync(input, async () =>
            {
                using (var tran = transaction.BeginTransaction())
                {
                    //创建订单
                    var order = new OrderFactory().Create(input.UserId, input.GoodsList.SetDto<OrderCreateGoodsReq, OrderItemEntity>());
                    orderRepository.Add(order);
                    //创建订单记录
                    var log = new OrderHandleLogFactory().Create(order.OrderNo, order.Id, order.State, input.UserId, input.Account, "");
                    orderHandleLogRepository.Add(log);
                    //持久化
                    await orderRepository.SaveAsync();
                    //发起订单创建成功事件
                    await eventBus.PublishAsync("EshopSample.CreateOrderSuccessEvent", new CreateOrderEvent(order.Id));
                    tran.Commit();
                    return true;
                }
            });
        }
    }
}
