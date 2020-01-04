using DomainBase;
using Goods.Domain.Aggregation;
using Goods.Domain.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goods.Domain.Service
{
    public class SaleGoodsCheckLegalService
    {
        public void LegalCheck(List<GoodsEntity> goodslist, ref List<SaleGoodsLegalServiceDto> sources)
        {
            if (goodslist.Count != sources.Count())
            {
                throw new DomainException("部分商品没有查询到");
            }
            foreach (var goods in goodslist)
            {
                var sourceGoods = sources.FirstOrDefault(x => x.GoodsId == goods.Id);
                //商品售卖
                goods.SaleGoods(sourceGoods.Count);
                sourceGoods.Name = goods.Name;
                sourceGoods.SinglePrice = goods.Price;
                sourceGoods.TotalPrice = goods.Price * sourceGoods.Count;
                sourceGoods.ImageId = goods.ImageId;
            }
        }
    }
}
