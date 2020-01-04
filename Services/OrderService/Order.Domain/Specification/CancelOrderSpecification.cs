using DomainBase;
using Order.Domain.Aggregation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Specification
{
    public class CancelOrderSpecification : IOperateSpecification<OrderEntity>
    {
        private Guid userId;
        public CancelOrderSpecification(Guid userId)
        {
            this.userId = userId;
        }
        public async Task<bool> SatisfiedBy(OrderEntity entity)
        {
            if (userId != entity.UserId)
                throw new DomainException("只有创建用户有权取消订单!");
            return await Task.FromResult(true);
        }
    }
}
