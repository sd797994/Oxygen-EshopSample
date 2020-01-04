using System;
using System.Collections.Generic;
using System.Text;

namespace OrderServiceInterface.Dtos
{
    public class QueryOrderDto
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public Guid Id { get; set; }
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
        public int State { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string Time { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 订单详情
        /// </summary>
        public List<OrderItemDto> OrderItems { get; set; }
        /// <summary>
        /// 订单日志
        /// </summary>
        public List<OrderLogsDto> orderLogs { get; set; }
    }
    public class OrderItemDto
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public decimal SinglePrice { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public decimal TotalPrice { get; set; }
        public string ImageId { get; set; }
        public string ImageCode { get; set; }
    }
    public class OrderLogsDto
    {
        public string LogContent { get; set; }
    }
}
