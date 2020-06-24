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
using Oxygen.DaprActorProvider;
using GoodsServiceInterface.Actor;
using GoodsServiceInterface.Actor.Dto;
using ApplicationBase.Infrastructure.Common;

namespace Goods.Application.UseCase
{
    public class UpdateGoodsBaseInfo : BaseUseCase<UpdateGoodsBaseInfoDto>, IUpdateGoodsBaseInfo
    {
        private readonly IServerProxyFactory serverProxyFactory;
        public UpdateGoodsBaseInfo(IServerProxyFactory serverProxyFactory, IIocContainer iocContainer) : base(iocContainer)
        {
            this.serverProxyFactory = serverProxyFactory;
        }
        public async Task<BaseApiResult<bool>> Excute(UpdateGoodsBaseInfoDto input)
        {
            return await HandleAsync(input, async () =>
            {
                //通过仓储获取商品聚合
                var goods = serverProxyFactory.CreateProxy<IGoodsActor>(input.Id);
                if (!await goods.Exists())
                {
                    throw new ApplicationException("没有找到该商品!");
                }
                //更新商品基本信息
                return await goods.UpdateBaseInfo(input.SetActorModel<UpdateGoodsBaseInfoDto, GoodsActorDto>(true));
            });
        }
    }
}
