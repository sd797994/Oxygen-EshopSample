using InfrastructureBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Trade.Infrastructure.PersistenceObject
{
    /// <summary>
    /// 交易信息表
    /// </summary>
    public class Trade : PersistenceObjectBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 用户余额
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
    }
}