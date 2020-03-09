using ApplicationBase;
using DomainBase;
using InfrastructureBase;
using Order.Domain.Aggregation;
using Order.Domain.Repository;
using Order.Infrastructure.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infrastructure.Repository
{
    public class OrderHandleLogRepository : RepositoryBase<OrderHandleLogEntity, PersistenceObject.OrderHandleLog, OrderContext>, IOrderHandleLogRepository
    {
        private readonly OrderContext _context;
        public OrderHandleLogRepository(
            OrderContext context, IIocContainer container) : base(context, container)
        {
            _context = context;
        }
    }
}
