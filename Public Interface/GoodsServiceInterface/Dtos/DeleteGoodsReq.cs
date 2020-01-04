using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoodsServiceInterface.Dtos
{
    public class DeleteGoodsReq : BaseAuthDto
    {
        [Required(ErrorMessage = "请输入商品ID")]
        public Guid Id { get; set; }
    }
}
