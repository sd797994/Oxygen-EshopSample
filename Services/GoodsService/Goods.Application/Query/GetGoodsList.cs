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
using ApplicationBase.Infrastructure.Common;

namespace Goods.Application.Query
{
    public class GetGoodsList : BaseQueryService<GoodsListQueryReq, PageQueryDto<GoodsQueryDto>>, IGetGoodsList
    {
        private readonly IGoodsRepository goodsRepository;
        public GetGoodsList(IGoodsRepository goodsRepository, IIocContainer iocContainer) : base(iocContainer)
        {
            this.goodsRepository = goodsRepository;
        }
        public override async Task<BaseApiResult<PageQueryDto<GoodsQueryDto>>> Query(GoodsListQueryReq input)
        {
            return await HandleAsync(input, async () =>
            {
                var result = await goodsRepository.PageQueryAsync(input.PageIndex, input.PageSize, new GoodsListQuerySpecification(input.Name, input.IsUpshelf),
                    input.OrderParms.ConvertToOrderExpression<GoodsEntity>()
                    );
                return new PageQueryDto<GoodsQueryDto>() { PageIndex = input.PageIndex, Data = result.Data.SetDto<GoodsEntity, GoodsQueryDto>(), Total = result.Total };
            });
        }
    }
}
