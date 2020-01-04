using ApplicationBase;
using BaseServcieInterface;
using Goods.Domain.Repository;
using Goods.Domain.Service;
using Goods.Domain.Service.Dto;
using GoodsServiceInterface.Dtos;
using GoodsServiceInterface.UseCase;
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
        private readonly IGoodsRepository goodsRepository;
        private readonly IGlobalTool globalTool;
        public WithholdingGoodsStock(IGoodsRepository goodsRepository, IGlobalTool globalTool, IIocContainer iocContainer) : base(iocContainer)
        {
            this.goodsRepository = goodsRepository;
            this.globalTool = globalTool;
        }
        public async Task<BaseApiResult<List<SaleGoodsDetail>>> Excute(WithholdingGoodsReq input)
        {
            return await HandleAsync(input, async () =>
            {
                //获取商品
                var goodsId = input.GoodsList.Select(y => y.GoodsId);
                var goods = await goodsRepository.GetManyAsync(x => goodsId.Contains(x.Id) && x.IsUpshelf);
                //调用领域服务检测商品有效性
                var goodsServiceDtos = globalTool.MapList<SaleGoodsDetail, SaleGoodsLegalServiceDto>(input.GoodsList);
                new SaleGoodsCheckLegalService().LegalCheck(goods, ref goodsServiceDtos);
                goodsRepository.UpdateRange(goods);
                //持久化
                await goodsRepository.SaveAsync();
                return globalTool.MapList<SaleGoodsLegalServiceDto, SaleGoodsDetail>(goodsServiceDtos);
            });
        }
    }
}
