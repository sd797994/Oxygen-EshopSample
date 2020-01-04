using System;
using System.Collections.Generic;
using System.Text;

namespace GoodsServiceInterface.Dtos
{
    public class GoodsQueryDto
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public Guid Id { get; set; }
        // <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 商品图片id
        /// </summary>
        public string ImageId { get; set; }
        /// <summary>
        /// 商品库存
        /// </summary>
        public int Stock { get; set; }
        /// <summary>
        /// 是否上架
        /// </summary>
        public bool IsUpshelf { get; set; }
    }
}
