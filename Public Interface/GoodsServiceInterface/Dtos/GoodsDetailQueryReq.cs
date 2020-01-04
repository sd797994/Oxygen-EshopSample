using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoodsServiceInterface.Dtos
{
    public class GoodsDetailQueryReq
    {
        [Required(ErrorMessage = "请填写商品ID")]
        public Guid GoodsId { get; set; }
    }
}
