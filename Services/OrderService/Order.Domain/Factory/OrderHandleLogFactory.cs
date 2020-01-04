using DomainBase;
using Order.Domain.Aggregation;
using Order.Domain.Aggregation.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Factory
{
    public class OrderHandleLogFactory
    {
        public OrderHandleLogEntity Create(string orderNo, Guid? orderId, OrderStateEnum? orderState, Guid? userId, string userName, string errMessage, bool isSystem = false)
        {
            if (orderId == null || orderState == null)
            {
                throw new DomainException("订单日志创建失败,必须包含订单信息!");
            }
            var log = new OrderHandleLogEntity()
            {
                Id = Guid.NewGuid(),
                UserId = userId.Value,
                UserName = userName ?? "",
                OrderNo = orderNo
            };
            if (!string.IsNullOrEmpty(errMessage))
            {
                log.LogContent = errMessage;
                log.HandleSuccess = false;
            }
            else
            {
                log.OrderId = orderId;
                log.HandleSuccess = true;
                switch (orderState)
                {
                    case OrderStateEnum.Create:
                        log.LogContent = $"用户[{userName}]于{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}创建了订单,订单编号[{orderNo}]";
                        break;
                    case OrderStateEnum.Cancel:
                        if (!isSystem)
                        {
                            log.LogContent = $"用户[{userName}]于{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}取消了订单,订单编号[{orderNo}]";
                        }
                        else
                        {
                            log.LogContent = $"由于超时未支付,系统于{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}主动取消了订单,订单编号[{orderNo}]";
                        }
                        break;
                    case OrderStateEnum.Pay:
                        log.LogContent = $"用户[{userName}]于{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}完成了订单支付,订单编号[{orderNo}]";
                        break;
                }
            }
            return log;
        }
    }
}
