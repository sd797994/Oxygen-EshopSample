using ApplicationBase;
using Order.Domain.Repository;
using OrderServiceInterface.Dtos;
using OrderServiceInterface.UseCase;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationException = ApplicationBase.ApplicationException;
using TradeServiceInterface.UseCase;
using TradeServiceInterface.Dtos;
using Order.Domain.Event;
using Order.Domain.Aggregation;
using Order.Domain.Factory;
using Order.Domain.Specification;

namespace Order.Application.UseCase
{
    public class OrderPay : BaseUseCase<OrderPayReq>, IOrderPay
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICurrentUserInfo currentUserInfo;
        private readonly IAccountRecharge accountRecharge;
        private readonly IOrderHandleLogRepository orderHandleLogRepository;
        private readonly IEventBus eventBus;
        private readonly ITransaction transaction;
        public OrderPay(IOrderRepository orderRepository, IOrderHandleLogRepository orderHandleLogRepository, IServiceProxy serviceProxy
            , IEventBus eventBus, ITransaction transaction, ICurrentUserInfo currentUserInfo, IIocContainer iocContainer) : base(iocContainer)
        {
            this.orderRepository = orderRepository;
            this.currentUserInfo = currentUserInfo;
            this.accountRecharge = serviceProxy.CreateProxy<IAccountRecharge>();
            this.eventBus = eventBus;
            this.orderHandleLogRepository = orderHandleLogRepository;
            this.transaction = transaction;
        }
        public async Task<BaseApiResult<bool>> Excute(OrderPayReq input)
        {
            bool needPublishEvent = false;
            OrderEntity order = default;
            return await HandleAsync(input, async () =>
            {
                //获取订单状态
                order = await orderRepository.GetAsync(x => x.UserId == currentUserInfo.UserId && x.Id == input.OrderId);
                if (order == null)
                {
                    throw new ApplicationException("订单错误,无法进行支付!");
                }
                //支付成功修改订单状态
                order.PayOrder();
                //创建订单记录
                var log = new OrderHandleLogFactory().Create(order.OrderNo, order.Id, order.State, input.UserId, input.Account, "", true);
                //规约检查
                if (await new PayOrderSpecification(input.UserId).SatisfiedBy(order))
                {
                    //rpc调用支付
                    var payResult = await accountRecharge.Excute(new AccountRechargeDto() { RechargeBalance = -order.TotalPrice, UserId = input.UserId, Account = input.Account });
                    if (payResult.IsError())
                    {
                        throw new ApplicationException(payResult.ErrMessage);
                    }
                    needPublishEvent = true;
                    //持久化
                    using (var tran = transaction.BeginTransaction())
                    {
                        orderRepository.Update(order);
                        orderHandleLogRepository.Add(log);
                        await orderRepository.SaveAsync();
                        tran.Commit();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }, async (e) =>
            {
                //当支付成功且修改订单状态失败则发布事件回滚支付
                if (needPublishEvent)
                    await eventBus.PublishAsync("EshopSample.PayOrderFailEvent", new PayOrderFailEvent() { TotalPrice = order.TotalPrice });
            });
        }
    }
}