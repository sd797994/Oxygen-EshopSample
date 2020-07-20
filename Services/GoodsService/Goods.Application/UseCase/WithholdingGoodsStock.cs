using ApplicationBase;
using ApplicationBase.Infrastructure.Common;
using BaseServcieInterface;
using Goods.Domain.Aggregation;
using Goods.Domain.Repository;
using Goods.Domain.Service;
using Goods.Domain.Service.Dto;
using GoodsServiceInterface.Actor;
using GoodsServiceInterface.Actor.Dto;
using GoodsServiceInterface.Dtos;
using GoodsServiceInterface.UseCase;
using Oxygen.DaprActorProvider;
using Oxygen.IServerProxyFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Goods.Application.UseCase
{
    public class WithholdingGoodsStock : BaseUseCase<WithholdingGoodsReq>, IWithholdingGoodsStock
    {
        private readonly IServerProxyFactory serverProxyFactory;
        public WithholdingGoodsStock(IServerProxyFactory serverProxyFactory, IIocContainer iocContainer) : base(iocContainer)
        {
            this.serverProxyFactory = serverProxyFactory;
        }
        public async Task<BaseApiResult<List<SaleGoodsDetail>>> Excute(WithholdingGoodsReq input)
        {
            return await HandleAsync(input, async () =>
            {
                var rollbackList = new List<(IGoodsActor goods, GoodsActorDto input)>();
                var goodsEntities = new List<GoodsEntity>();
                var needRollback = false;
                input.GoodsList.ForEach(async detail =>
                {
                    var goods = serverProxyFactory.CreateProxy<IGoodsActor>(detail.GoodsId);
                    var goodsEntity = await goods.Get();
                    if (goodsEntity == null)
                    {
                        needRollback = true;
                        return;
                    }
                    else
                    {
                        goodsEntities.Add(goodsEntity);
                        var param = input.SetActorModel<WithholdingGoodsReq, GoodsActorDto>(true);
                        if (await goods.WithholdingGoodsStock(param))
                        {
                            rollbackList.Add((goods, param));
                        }
                        else
                        {
                            needRollback = true;
                            return;
                        }
                    }
                });
                if (needRollback && rollbackList.Any())
                {
                    rollbackList.ForEach(x => { x.input.Rollback = true; x.goods.WithholdingGoodsStock(x.input); });
                    throw new ApplicationBase.ApplicationException("部分商品库存不足,请刷新后重试");
                }
                var result = goodsEntities.SetDto<List<GoodsEntity>, List<SaleGoodsDetail>>();
                result.ForEach(x => x.TotalPrice = x.SinglePrice * x.StockNumber);
                return result;
            });
        }
    }
}
