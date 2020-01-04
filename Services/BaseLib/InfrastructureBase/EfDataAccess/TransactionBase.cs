using ApplicationBase;
using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Oxygen.CommonTool;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureBase
{
    public abstract class TransactionBase<TContext> : ITransaction where TContext : DbContext
    {
        private IDbContextTransaction contextTransaction;
        private readonly TContext context;
        public TransactionBase(TContext context)
        {
            this.context = context;
        }
        public ITransaction BeginTransaction()
        {
            contextTransaction = context.Database.BeginTransaction(IocContainer.Resolve<ICapPublisher>());
            return this;
        }

        public void Commit()
        {
            contextTransaction.Commit();
        }
        public void Dispose()
        {

        }
    }
}
