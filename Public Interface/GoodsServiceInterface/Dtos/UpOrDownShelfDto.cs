using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoodsServiceInterface.Dtos
{
    public class UpOrDownShelfDto : BaseAuthDto
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Required(ErrorMessage = "请输入商品ID")]
        public Guid Id { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        [Required(ErrorMessage = "是否上架不能为空")]
        public bool IsUpShelf { get; set; }
    }
}
