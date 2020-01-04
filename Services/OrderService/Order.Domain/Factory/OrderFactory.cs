using Order.Domain.Aggregation;
using Order.Domain.Aggregation.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Order.Domain.Factory
{
    public class OrderFactory
    {
        public OrderEntity Create(Guid userId, List<OrderItemEntity> orderItems)
        {
            var order = new OrderEntity()
            {
                Id = Guid.NewGuid(),
                OrderNo = DateTime.Now.ToString("yyyyMMddHHmmss") + Guid.NewGuid().ToString().Substring(0, 6),
                State = OrderStateEnum.Create,
                UserId = userId,
                CreateTime = DateTime.Now
            };
            orderItems.ForEach(x =>
            {
                x.Id = Guid.NewGuid();
                x.OrderId = order.Id;
            });
            order.OrderItems = orderItems;
            order.TotalPrice = orderItems.Sum(x => x.TotalPrice);
            return order;
        }
    }
}
