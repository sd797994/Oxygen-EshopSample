using ApplicationBase;
using Goods.Domain.Repository;
using GoodsServiceInterface.Dtos;
using GoodsServiceInterface.UseCase;
using Microsoft.Extensions.Logging;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationException = ApplicationBase.ApplicationException;
using GoodsServiceInterface.Actor;
using Oxygen.IServerProxyFactory;
using Oxygen.DaprActorProvider;
using ApplicationBase.Infrastructure.Common;
using GoodsServiceInterface.Actor.Dto;

namespace Goods.Application.UseCase
{
    public class IncreaseGoods : BaseUseCase<IncreaseGoodsDto>, IIncreaseGoods
    {
        private readonly IServerProxyFactory serverProxyFactory;
        public IncreaseGoods(IServerProxyFactory serverProxyFactory, IIocContainer iocContainer) : base(iocContainer)
        {
            this.serverProxyFactory = serverProxyFactory;
        }
        public async Task<BaseApiResult<bool>> Excute(IncreaseGoodsDto input)
        {
            return await HandleAsync(input, async () =>
            {
                //通过仓储获取商品聚合
                var goods = serverProxyFactory.CreateProxy<IGoodsActor>(input.Id);
                if (!await goods.Exists())
                {
                    throw new ApplicationException("没有找到该商品!");
                }
                //商品修改库存
                await goods.IncreaseGoods(input.SetActorModel<IncreaseGoodsDto, GoodsActorDto>(true));
                return true;
            });
        }
    }
}
