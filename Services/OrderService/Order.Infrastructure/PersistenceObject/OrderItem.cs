using InfrastructureBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Order.Infrastructure.PersistenceObject
{
    public class OrderItem : PersistenceObjectBase
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }
        /// <summary>
        /// 商品ID
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
        /// 商品图片编号
        /// </summary>
        public string ImageId { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal SinglePrice { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
    }
}
