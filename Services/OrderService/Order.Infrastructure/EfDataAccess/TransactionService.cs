using ApplicationBase;
using DotNetCore.CAP;
using InfrastructureBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infrastructure.EfDataAccess
{
    public class TransactionService : TransactionBase<OrderContext>, ITransaction
    {
        public TransactionService(OrderContext context) : base(context)
        {

        }
    }
}
