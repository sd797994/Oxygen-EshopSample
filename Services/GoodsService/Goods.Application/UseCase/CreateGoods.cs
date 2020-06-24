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
using Oxygen.IServerProxyFactory;
using GoodsServiceInterface.Actor;
using Oxygen.DaprActorProvider;

namespace Goods.Application.UseCase
{
    public class CreateGoods : BaseUseCase<CreateGoodsDto>, ICreateGoods
    {
        private readonly IGoodsRepository goodsRepository;
        private readonly IServerProxyFactory serverProxyFactory;
        public CreateGoods(IGoodsRepository goodsRepository, IServerProxyFactory serverProxyFactory, IIocContainer iocContainer) : base(iocContainer)
        {
            this.goodsRepository = goodsRepository;
            this.serverProxyFactory = serverProxyFactory;
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
                    var proxy = serverProxyFactory.CreateProxy<IGoodsActor>(goods.Id);
                    await proxy.Add(goods);//创建actor对象到缓存里
                    return true;
                }
                return false;
            });
        }
    }
}

