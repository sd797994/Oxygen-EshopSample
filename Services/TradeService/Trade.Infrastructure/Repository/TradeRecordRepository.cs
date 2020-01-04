using ApplicationBase;
using InfrastructureBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trade.Domain.Aggregation;
using Trade.Domain.Repository;
using Trade.Infrastructure.EfDataAccess;

namespace Trade.Infrastructure.Repository
{
    public class TradeRecordRepository : RepositoryBase<TradeRecordEntity, PersistenceObject.TradeRecord,TradeContext>, ITradeRecordRepository
    {
        private readonly TradeContext context;
        public TradeRecordRepository(
            TradeContext context, ICurrentUserInfo currentUser) : base(context, currentUser)
        {
            this.context = context;
        }
    }
}
