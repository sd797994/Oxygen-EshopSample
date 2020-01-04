using InfrastructureBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Goods.Infrastructure.PersistenceObject
{
    public class Goods: PersistenceObjectBase
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        /// <summary>
        /// 商品库存
        /// </summary>
        public int Stock { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string ImageId { get; set; }
        /// <summary>
        /// 是否上架
        /// </summary>
        public bool IsUpshelf { get; set; }
        /// <summary>
        /// 上架时间
        /// </summary>
        public DateTime? UpshelfTime { get; set; }
    }
}
