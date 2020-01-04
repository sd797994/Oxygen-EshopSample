using InfrastructureBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Trade.Domain.Aggregation.ValueObject;

namespace Trade.Infrastructure.PersistenceObject
{
    /// <summary>
    /// 用户交易记录表
    /// </summary>
    public class TradeRecord : PersistenceObjectBase
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
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime TradeTime { get; set; }
    }
}
