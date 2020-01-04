using System;
using System.Collections.Generic;
using System.Text;

namespace Trade.Domain.Aggregation.ValueObject
{
    public enum UserStateEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 冻结
        /// </summary>
        Frozen = 1,
    }
}
