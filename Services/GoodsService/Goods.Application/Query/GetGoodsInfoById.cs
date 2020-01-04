using ApplicationBase;
using Goods.Domain.Repository;
using GoodsServiceInterface.Dtos;
using GoodsServiceInterface.Query;
using Microsoft.Extensions.Logging;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Application.Query
{
    public class GetGoodsInfoById : BaseQueryService<GetGoodsInfoByIdReq, List<GetGoodsInfoByIdDto>>, IGetGoodsInfoById
    {
        private readonly IGoodsRepository goodsRepository;
        public GetGoodsInfoById(IGoodsRepository goodsRepository, IIocContainer iocContainer) : base(iocContainer)
        {
            this.goodsRepository = goodsRepository;
        }
        public override async Task<BaseApiResult<List<GetGoodsInfoByIdDto>>> Query(GetGoodsInfoByIdReq input)
        {
            return await HandleAsync(input, async () =>
            {
                var goods = await goodsRepository.GetManyAsync(x => input.GoodsIds.Contains(x.Id) && x.IsUpshelf);
                if (goods.Count > 0)
                {
                    return goods.Select(x => new GetGoodsInfoByIdDto() { GoodsId = x.Id, GoodsName = x.Name, Stock = x.Stock }).ToList();
                }
                return new List<GetGoodsInfoByIdDto>();
            });
        }
    }
}
