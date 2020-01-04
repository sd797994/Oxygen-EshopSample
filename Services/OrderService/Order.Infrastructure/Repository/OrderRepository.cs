using Order.Domain.Aggregation;
using Order.Domain.Repository;
using Order.Infrastructure.EfDataAccess;
using InfrastructureBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationBase;
using System.Threading.Tasks;
using System.Linq.Expressions;
using DomainBase;

namespace Order.Infrastructure.Repository
{
    public class OrderRepository : RepositoryBase<OrderEntity, PersistenceObject.Order, OrderContext>, IOrderRepository
    {
        private readonly OrderContext _context;
        public OrderRepository(
            OrderContext context, ICurrentUserInfo currentUser) : base(context, currentUser)
        {
            _context = context;
        }

        public override bool Add(OrderEntity order)
        {
            _context.Order.Add(GetPersistentObject(order, true));
            _context.OrderItem.AddRange(GetPersistentObjectList<OrderItemEntity, PersistenceObject.OrderItem>(order.OrderItems, true));
            return true;
        }

        public override async Task<OrderEntity> GetAsync(object key)
        {
            var orderPo = await base.GetAsync(key);
            if (orderPo != null)
            {
                var OrderItemsPo = _context.OrderItem.Where(x => x.OrderId == orderPo.Id).ToList();
                if (OrderItemsPo != null && OrderItemsPo.Any())
                {
                    orderPo.OrderItems = GetDomainEntityList<PersistenceObject.OrderItem, OrderItemEntity>(OrderItemsPo);
                }
            }
            return orderPo;
        }
        public override async Task<OrderEntity> GetAsync(Expression<Func<OrderEntity, bool>> specification, bool asNoTracking = true)
        {
            var orderPo = await base.GetAsync(specification);
            if (orderPo != null)
            {
                var OrderItemsPo = _context.OrderItem.Where(x => !x.IsDeleted && x.OrderId == orderPo.Id).ToList();
                if (OrderItemsPo != null && OrderItemsPo.Any())
                {
                    orderPo.OrderItems = GetDomainEntityList<PersistenceObject.OrderItem, OrderItemEntity>(OrderItemsPo);
                }
            }
            return orderPo;
        }

        public override async Task<List<OrderEntity>> GetManyAsync(ISpecification<OrderEntity> specification, bool asNoTracking = true, params (Expression<Func<OrderEntity, dynamic>>, bool)[] orderbys)
        {
            var orderPoList = await base.GetManyAsync(specification, asNoTracking, orderbys);
            if (orderPoList.Any())
            {
                var orderIds = orderPoList.Select(x => x.Id).ToList();
                var itemList = _context.OrderItem.Where(x => !x.IsDeleted && orderIds.Contains(x.OrderId));
                orderPoList.ForEach(x => x.OrderItems = GetDomainEntityList<PersistenceObject.OrderItem, OrderItemEntity>(itemList.Where(y => y.OrderId == x.Id).ToList()));
            }
            return orderPoList;
        }
        public override async Task<List<OrderEntity>> GetManyAsync(Expression<Func<OrderEntity, bool>> specification, bool asNoTracking = true, params (Expression<Func<OrderEntity, dynamic>>, bool)[] orderbys)
        {
            var orderPoList = await base.GetManyAsync(specification, asNoTracking, orderbys);
            if (orderPoList.Any())
            {
                var orderIds = orderPoList.Select(x => x.Id).ToList();
                var itemList = _context.OrderItem.Where(x => !x.IsDeleted && orderIds.Contains(x.OrderId));
                orderPoList.ForEach(x => x.OrderItems = GetDomainEntityList<PersistenceObject.OrderItem, OrderItemEntity>(itemList.Where(y => y.OrderId == x.Id).ToList()));
            }
            return orderPoList;
        }
    }
}
