using DomainBase;
using Order.Domain.Aggregation.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Aggregation
{
    /// <summary>
    /// 订单实体
    /// </summary>
    public class OrderEntity : AggregateRoot
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单总价
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStateEnum State { get; set; }
        /// <summary>
        /// 下单用户Id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 订单详情
        /// </summary>
        public List<OrderItemEntity> OrderItems { get; set; }

        public void CancelOrder()
        {
            switch (State)
            {
                case OrderStateEnum.Create:
                    State = OrderStateEnum.Cancel;
                    break;
                case OrderStateEnum.Pay:
                case OrderStateEnum.Cancel:
                    throw new DomainException("已支付/与取消的订单无法再次取消");
            }
        }

        public void PayOrder()
        {
            switch (State)
            {
                case OrderStateEnum.Create:
                    State = OrderStateEnum.Pay;
                    break;
                case OrderStateEnum.Pay:
                case OrderStateEnum.Cancel:
                    throw new DomainException("已支付/与取消的订单无法再次支付");
            }
        }
    }
}
