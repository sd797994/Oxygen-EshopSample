using ApplicationBase;
using ApplicationBase.Infrastructure.Common;
using BaseServcieInterface;
using Order.Domain.Aggregation;
using Order.Domain.Aggregation.ValueObject;
using Order.Domain.Repository;
using OrderServiceInterface.Dtos;
using OrderServiceInterface.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Query
{
    public class GetOrderListByUser : BaseQueryService<QueryOrderReq, List<QueryOrderDto>>, IGetOrderListByUser
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderHandleLogRepository orderHandleLogRepository;
        private readonly ICurrentUserInfo currentUser;
        public GetOrderListByUser(IOrderRepository orderRepository, IOrderHandleLogRepository orderHandleLogRepository,ICurrentUserInfo currentUser, IIocContainer iocContainer) : base(iocContainer)
        {
            this.orderRepository = orderRepository;
            this.orderHandleLogRepository = orderHandleLogRepository;
            this.currentUser = currentUser;
        }
        public override async Task<BaseApiResult<List<QueryOrderDto>>> Query(QueryOrderReq input)
        {
            return await HandleAsync(input, async () =>
            {
                var data = await orderRepository.GetManyAsync(x => input.OrderType == 0 ? (x.State != 0) : x.State == (OrderStateEnum)input.OrderType && x.UserId == currentUser.UserId, true, input.OrderParms.ConvertToOrderExpression<OrderEntity>());
                var result = data.SetDto<OrderEntity, QueryOrderDto>();
                var orderIds = result.Select(x => x.Id).ToList();
                var logs = await orderHandleLogRepository.GetManyAsync(x => orderIds.Contains(x.OrderId.Value));
                result.ForEach(x =>
                {
                    x.Time = x.CreateTime.ToString("yyyy-MM-dd HH:mm");
                    x.State = (int)data.First(y => y.OrderNo == x.OrderNo).State;
                    x.orderLogs = logs.Where(y => y.OrderId == x.Id).SetDto<OrderHandleLogEntity, OrderLogsDto>();
                });
                return result;
            });
        }
    }
}
