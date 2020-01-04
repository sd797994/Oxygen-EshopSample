using ApplicationBase;
using Goods.Domain.Factory;
using Goods.Domain.Repository;
using Goods.Domain.Specification;
using GoodsServiceInterface.Dtos;
using GoodsServiceInterface.UseCase;
using Microsoft.Extensions.Logging;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationException = ApplicationBase.ApplicationException;

namespace Goods.Application.UseCase
{
    public class CreateGoods : BaseUseCase<CreateGoodsDto>, ICreateGoods
    {
        private readonly IGoodsRepository goodsRepository;
        public CreateGoods(IGoodsRepository goodsRepository, IIocContainer iocContainer) : base(iocContainer)
        {
            this.goodsRepository = goodsRepository;
        }
        public async Task<BaseApiResult<bool>> Excute(CreateGoodsDto input)
        {
            return await HandleAsync(input, async () =>
            {
                //工厂创建商品聚合
                var goods = new GoodsFactory().Create(input.Name, input.Price,input.ImageId, input.Stock);
                //创建商品规约检查
                if (await new CreateGoodsSpecification(goodsRepository).SatisfiedBy(goods))
                {
                    //仓储添加
                    goodsRepository.Add(goods);
                    await goodsRepository.SaveAsync();
                    return true;
                }
                //持久化
                return false;
            });
        }
    }
}

