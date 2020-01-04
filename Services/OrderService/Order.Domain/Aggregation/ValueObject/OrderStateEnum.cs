using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Aggregation.ValueObject
{
    public enum OrderStateEnum
    {
        Create = 1,
        Pay = 2,
        Cancel = 3
    }
}
