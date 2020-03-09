using ApplicationBase;
using Goods.Domain.Aggregation;
using Goods.Domain.Repository;
using Goods.Infrastructure.EfDataAccess;
using InfrastructureBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goods.Infrastructure.Repository
{
    public class GoodsRepository : RepositoryBase<GoodsEntity, PersistenceObject.Goods, GoodsContext>, IGoodsRepository
    {
        private readonly GoodsContext _context;
        public GoodsRepository(
            GoodsContext context, IIocContainer container) : base(context, container)
        {
            _context = context;
        }
    }
}
