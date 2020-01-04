using System;
using System.Collections.Generic;
using System.Text;

namespace Goods.Domain.Service.Dto
{
    public class SaleGoodsLegalServiceDto
    {
        public Guid GoodsId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal SinglePrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string ImageId { get; set; }
    }
}
