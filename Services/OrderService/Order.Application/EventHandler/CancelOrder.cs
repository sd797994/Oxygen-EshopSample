using ApplicationBase;
using Order.Domain.Aggregation;
using Order.Domain.Event;
using Order.Domain.Factory;
using Order.Domain.Repository;
using Order.Domain.Specification;
using OrderServiceInterface.Dtos;
using OrderServiceInterface.UseCase;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationException = ApplicationBase.ApplicationException;

namespace Order.Application.UseCase
{
    public class CancelOrder : BaseEventHandler<CancelOrderDto>, IEventHandler
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderHandleLogRepository orderHandleLogRepository;
        private readonly IEventBus eventBus;
        private readonly IGlobalTool globalTool;
        private readonly ITransaction transaction;
        public CancelOrder(IOrderRepository orderRepository, IOrderHandleLogRepository orderHandleLogRepository, IEventBus eventBus, IGlobalTool globalTool, ITransaction transaction, IIocContainer iocContainer) : base(iocContainer)
        {
            this.orderRepository = orderRepository;
            this.orderHandleLogRepository = orderHandleLogRepository;
            this.eventBus = eventBus;
            this.globalTool = globalTool;
            this.transaction = transaction;
        }
        [EventHandler("EshopSample.TimeOutCancelOrderEvent")]
        public override async Task Handle(CancelOrderDto input)
        {
            await HandleAsync(input, async () =>
            {
                using (var tran = transaction.BeginTransaction())
                {
                    var order = await orderRepository.GetAsync(input.OrderId);
                    if (order == null)
                    {
                        throw new ApplicationException("订单没有找到!");
                    }
                    //取消订单操作
                    order.CancelOrder();
                    //创建订单记录
                    var log = new OrderHandleLogFactory().Create(order.OrderNo, order.Id, order.State, input.UserId, input.Account, "", true);
                    //规约检查
                    if (await new CancelOrderSpecification(input.UserId).SatisfiedBy(order))
                    {
                        //持久化
                        orderRepository.Update(order);
                        orderHandleLogRepository.Add(log);
                        //发起取消订单成功事件
                        await eventBus.PublishAsync("EshopSample.CancelOrderSuccessEvent", new CancelOrderEvent(order.Id, globalTool.MapList<OrderItemEntity, OrderGoodsEvent>(order.OrderItems)) { });
                        await orderRepository.SaveAsync();
                        tran.Commit();
                    }
                }
            });
        }
    }
}
