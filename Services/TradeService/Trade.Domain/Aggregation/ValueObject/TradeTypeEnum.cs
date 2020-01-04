using System;
using System.Collections.Generic;
using System.Text;

namespace Trade.Domain.Aggregation.ValueObject
{
    public enum TradeTypeEnum
    {
        /// <summary>
        /// 充值
        /// </summary>
        Recharge = 1,
        /// <summary>
        /// 扣款
        /// </summary>
        Deduction = 2,
    }
}
