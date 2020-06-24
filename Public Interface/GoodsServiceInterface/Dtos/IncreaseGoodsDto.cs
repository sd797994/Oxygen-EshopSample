using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoodsServiceInterface.Dtos
{
    public class IncreaseGoodsDto : BaseAuthDto
    {
        [Required(ErrorMessage = "请输入商品ID")]
        public Guid Id { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "库存超出范围")]
        [Required(ErrorMessage = "请输入库存")]
        public int StockNumber { get; set; }
    }
}
