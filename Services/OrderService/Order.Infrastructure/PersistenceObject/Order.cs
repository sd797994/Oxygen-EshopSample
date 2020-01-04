using InfrastructureBase;
using Order.Domain.Aggregation.ValueObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Order.Infrastructure.PersistenceObject
{
    public class Order: PersistenceObjectBase
    {
        // <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单总价
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStateEnum State { get; set; }
        /// <summary>
        /// 下单用户Id
        /// </summary>
        public Guid UserId { get; set; }
    }
}
