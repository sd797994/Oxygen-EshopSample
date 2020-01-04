using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoodsServiceInterface.Dtos
{
    public class UpdateGoodsBaseInfoDto : BaseAuthDto
    {

        [Required(ErrorMessage = "请填写商品ID")]
        public Guid Id { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [Required(ErrorMessage = "商品名称不能为空")]
        [MaxLength(20, ErrorMessage = "商品名称长度不能超过20位")]
        public string Name { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        [Required(ErrorMessage = "商品价格不能为空")]
        [Range(0, double.MaxValue, ErrorMessage = "商品价格超出范围")]
        public decimal Price { get; set; }
    }
}
