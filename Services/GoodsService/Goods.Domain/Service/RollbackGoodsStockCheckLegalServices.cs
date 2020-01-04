using DomainBase;
using Goods.Domain.Aggregation;
using Goods.Domain.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goods.Domain.Service
{
    public class RollbackGoodsStockCheckLegalServices
    {
        public void LegalCheck(ref List<GoodsEntity> goodslist, List<SaleGoodsLegalServiceDto> sources)
        {
            if (goodslist.Count != sources.Count())
            {
                throw new DomainException("部分商品没有查询到");
            }
            foreach (var goods in goodslist)
            {
                var sourceGoods = sources.FirstOrDefault(x => x.GoodsId == goods.Id);
                //商品库存回滚
                goods.RollbackGoods(sourceGoods.Count);
            }
        }
    }
}
