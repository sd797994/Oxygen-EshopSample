using DomainBase;
using Goods.Domain.Aggregation;
using Goods.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Domain.Specification
{
    public class CreateGoodsSpecification : IOperateSpecification<GoodsEntity>
    {
        private readonly IGoodsRepository goodsRepository;
        public CreateGoodsSpecification(IGoodsRepository goodsRepository)
        {
            this.goodsRepository = goodsRepository;
        }
        public async Task<bool> SatisfiedBy(GoodsEntity entity)
        {
            if (await this.goodsRepository.AnyAsync(x => x.Name.Equals(entity.Name)))
            {
                throw new DomainException("当前已有同名商品,不可重复创建!");
            }
            return true;
        }
    }
}
