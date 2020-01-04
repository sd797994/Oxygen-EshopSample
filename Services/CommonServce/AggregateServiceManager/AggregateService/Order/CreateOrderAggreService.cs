using AggregateServiceManager.ServiceRoute;
using OrderServiceInterface.Dtos;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoodsServiceInterface.UseCase;
using InfrastructureBase;
using GoodsServiceInterface.Dtos;
using OrderServiceInterface.UseCase;
using ApplicationException = ApplicationBase.ApplicationException;
using ApplicationBase;

namespace AggregateServiceManager.Order
{
    public class CreateOrderAggreService : AggreServiceBase
    {
        private readonly IWithholdingGoodsStock withholdingGoodsStock;
        private readonly IOrderCreate orderCreate;
        private readonly IEventBus eventBus;
        public CreateOrderAggreService() : base("/api/OrderService/OrderCreate/Excute", typeof(OrderCreateReq), true)
        {
            withholdingGoodsStock = IocContainer.Resolve<IWithholdingGoodsStock>();
            orderCreate = IocContainer.Resolve<IOrderCreate>();
            eventBus = IocContainer.Resolve<IEventBus>();
        }
        public override async Task<BaseApiResult<object>> Process(object input)
        {
            bool needPublishEvent = false;
            return await HandleAsync(async () =>
            {
                var value = (OrderCreateReq)input;
                //rpc调用商品预扣库存
                var withholdStock = await withholdingGoodsStock.Excute(value.Mapper<OrderCreateReq, WithholdingGoodsReq>());
                if (withholdStock.IsError())
                {
                    throw new ApplicationException(withholdStock.ErrMessage);
                }
                //rpc调用订单创建订单
                value.GoodsList = withholdStock.Data.MapperList<SaleGoodsDetail, OrderCreateGoodsReq>();
                var createOrder = await orderCreate.Excute(value);
                if (createOrder.IsError())
                {
                    needPublishEvent = true;
                    throw new ApplicationException(createOrder.ErrMessage);
                }
                return true;
            }, async (e) =>
            {
                //失败发布订单创建失败事件,商品服务订阅事件并回滚库存
                if (needPublishEvent)
                    await eventBus.PublishAsync("EshopSample.CreateOrderFailEvent", (OrderCreateReq)input);
            });
        }
    }
}
