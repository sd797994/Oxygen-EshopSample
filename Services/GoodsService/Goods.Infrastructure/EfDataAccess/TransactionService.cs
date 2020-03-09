using ApplicationBase;
using DotNetCore.CAP;
using InfrastructureBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Goods.Infrastructure.EfDataAccess
{
    public class TransactionService : TransactionBase<GoodsContext>, ITransaction
    {
        public TransactionService(GoodsContext context, IIocContainer container) : base(context, container)
        {

        }
    }
}
