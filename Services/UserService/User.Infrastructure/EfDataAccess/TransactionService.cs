using ApplicationBase;
using DotNetCore.CAP;
using InfrastructureBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.Infrastructure.EfDataAccess
{
    public class TransactionService : TransactionBase<UserContext>, ITransaction
    {
        public TransactionService(UserContext context) : base(context)
        {

        }
    }
}
