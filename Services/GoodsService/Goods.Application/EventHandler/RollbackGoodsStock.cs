using ApplicationBase;
using Goods.Domain.Repository;
using Goods.Domain.Service;
using Goods.Domain.Service.Dto;
using GoodsServiceInterface.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Application.EventHandler
{
    public class RollbackGoodsStock : BaseEventHandler<SaleGoodsDto>, IEventHandler
    {
        private readonly IGoodsRepository goodsRepository;
        private readonly IGlobalTool globalTool;
        public RollbackGoodsStock(IGoodsRepository goodsRepository, ITransaction transaction, IGlobalTool globalTool, IIocContainer iocContainer) : base(iocContainer)
        {
            this.goodsRepository = goodsRepository;
            this.globalTool = globalTool;
        }
        [EventHandler("EshopSample.CancelOrderSuccessEvent")]
        [EventHandler("EshopSample.CreateOrderFailEvent")]
        public override async Task Handle(SaleGoodsDto input)
        {
            await HandleAsync(input, async () =>
            {
                //获取商品
                var goodsId = input.Goodslist.Select(y => y.GoodsId);
                var goods = await goodsRepository.GetManyAsync(x => goodsId.Contains(x.Id) && x.IsUpshelf);
                //调用领域服务检测商品有效性
                var goodsServiceDtos = globalTool.MapList<SaleGoodsDetail, SaleGoodsLegalServiceDto>(input.Goodslist);
                new RollbackGoodsStockCheckLegalServices().LegalCheck(ref goods, goodsServiceDtos);
                //持久化
                goodsRepository.UpdateRange(goods);
                await goodsRepository.SaveAsync();
            });
        }
    }
}
