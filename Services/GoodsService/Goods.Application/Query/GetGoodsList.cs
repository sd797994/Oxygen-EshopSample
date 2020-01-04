using ApplicationBase;
using DomainBase;
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
using Goods.Domain.Specification;

namespace Goods.Application.Query
{
    public class GetGoodsList : BaseQueryService<GoodsListQueryReq, PageQueryDto<GoodsQueryDto>>, IGetGoodsList
    {
        private readonly IGoodsRepository goodsRepository;
        private readonly IGlobalTool globalTool;
        public GetGoodsList(IGoodsRepository goodsRepository, IGlobalTool globalTool, IIocContainer iocContainer) : base(iocContainer)
        {
            this.goodsRepository = goodsRepository;
            this.globalTool = globalTool;
        }
        public override async Task<BaseApiResult<PageQueryDto<GoodsQueryDto>>> Query(GoodsListQueryReq input)
        {
            return await HandleAsync(input, async () =>
            {
                var result = await goodsRepository.PageQueryAsync(input.PageIndex, input.PageSize,new GoodsListQuerySpecification(input.Name,input.IsUpshelf), input.OrderParms.ConvertToOrderExpression<GoodsEntity>());
                var result2 = new PageQueryDto<GoodsQueryDto>() { PageIndex = input.PageIndex, Data = globalTool.MapList<GoodsEntity, GoodsQueryDto>(result.Data), Total = result.Total };
                return result2;
            });
        }
    }
}
