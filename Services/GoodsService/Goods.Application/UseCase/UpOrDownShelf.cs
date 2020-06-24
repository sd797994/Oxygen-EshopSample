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
using Oxygen.IServerProxyFactory;
using GoodsServiceInterface.Actor;
using Oxygen.DaprActorProvider;
using ApplicationBase.Infrastructure.Common;
using GoodsServiceInterface.Actor.Dto;

namespace Goods.Application.UseCase
{
    public class UpOrDownShelf : BaseUseCase<UpOrDownShelfDto>, IUpOrDownShelf
    {
        private readonly IServerProxyFactory serverProxyFactory;
        public UpOrDownShelf(IServerProxyFactory serverProxyFactory, IIocContainer iocContainer) : base(iocContainer)
        {
            this.serverProxyFactory = serverProxyFactory;
        }
        public async Task<BaseApiResult<bool>> Excute(UpOrDownShelfDto input)
        {
            return await HandleAsync(input, async () =>
            {
                //通过仓储获取商品聚合
                var goods = serverProxyFactory.CreateProxy<IGoodsActor>(input.Id);
                if (!await goods.Exists())
                {
                    throw new ApplicationException("没有找到该商品!");
                }
                //上下架商品
                return await goods.UpOrDownShelf(input.SetActorModel<UpOrDownShelfDto, GoodsActorDto>(true));
            });
        }
    }
}