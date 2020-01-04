using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoodsServiceInterface.Dtos
{
    public class WithholdingGoodsReq
    {
        [MinLength(1, ErrorMessage = "必须选择一个商品")]
        public List<SaleGoodsDetail> GoodsList { get; set; }
    }
}
