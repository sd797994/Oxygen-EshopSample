using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;
using Trade.Domain.Aggregation.ValueObject;

namespace Trade.Domain.Aggregation
{
    public class TradeRecordEntity : AggregateRoot
    {
        /// <summary>
        /// 交易Id
        /// </summary>
        public Guid TradeId { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public TradeTypeEnum TradeType { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime TradeTime { get; set; }
    }
}
