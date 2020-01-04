using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OrderServiceInterface.Dtos
{
    public class OrderCreateReq: BaseAuthDto
    {
        [MinLength(1,ErrorMessage = "必须选择一个商品")]
        public List<OrderCreateGoodsReq> GoodsList { get; set; }
    }
    public class OrderCreateGoodsReq
    {
        [Required(ErrorMessage = "必须选择一个商品")]
        public Guid GoodsId { get; set; }
        [Required(ErrorMessage = "必须填写商品数量")]
        [Range(1, int.MaxValue, ErrorMessage = "商品数量必须大于等于1")]
        public int Count { get; set; }
        public string Name { get; set; }
        public decimal SinglePrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string ImageId { get; set; }
    }
}
