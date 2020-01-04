using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Aggregation
{
    /// <summary>
    /// 订单详情
    /// </summary>
    public class OrderItemEntity : Entity
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public Guid OrderId { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public Guid GoodsId { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public decimal SinglePrice { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 商品图片编号
        /// </summary>
        public string ImageId { get; set; }
    }
}
