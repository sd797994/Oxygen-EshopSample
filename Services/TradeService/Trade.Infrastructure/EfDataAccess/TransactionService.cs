using ApplicationBase;
using DotNetCore.CAP;
using InfrastructureBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trade.Infrastructure.EfDataAccess
{
    public class TransactionService : TransactionBase<TradeContext>, ITransaction
    {
        public TransactionService(TradeContext context) : base(context)
        {

        }
    }
}
