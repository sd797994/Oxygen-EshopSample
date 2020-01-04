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
    public class TradeRepository : RepositoryBase<TradeEntity, PersistenceObject.Trade, TradeContext>, ITradeRepository
    {
        private readonly TradeContext context;
        public TradeRepository(
            TradeContext context, ICurrentUserInfo currentUser) : base(context, currentUser)
        {
            this.context = context;
        }
    }
}
