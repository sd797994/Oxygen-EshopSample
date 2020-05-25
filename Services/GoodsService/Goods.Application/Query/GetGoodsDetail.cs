using ApplicationBase;
using Goods.Domain.Aggregation;
using Goods.Domain.Repository;
using GoodsServiceInterface.Dtos;
using GoodsServiceInterface.Query;
using Microsoft.Extensions.Logging;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationException = ApplicationBase.ApplicationException;
using ApplicationBase.Infrastructure.Common;

namespace Goods.Application.Query
{
    public class GetGoodsDetail : BaseQueryService<GoodsDetailQueryReq, GoodsQueryDto>, IGetGoodsDetail
    {
        private readonly IGoodsRepository goodsRepository;
        public GetGoodsDetail(IGoodsRepository goodsRepository, IIocContainer iocContainer) : base(iocContainer)
        {
            this.goodsRepository = goodsRepository;
        }
        public override async Task<BaseApiResult<GoodsQueryDto>> Query(GoodsDetailQueryReq input)
        {
            return await HandleAsync(input, async () =>
            {
                //通过仓储获取商品聚合
                var goods = await goodsRepository.GetAsync(x => x.Id == input.GoodsId);
                if (goods == null)
                {
                    throw new ApplicationException("没有找到该商品!");
                }
                return goods.SetDto<GoodsEntity, GoodsQueryDto>();
            });
        }
    }
}
