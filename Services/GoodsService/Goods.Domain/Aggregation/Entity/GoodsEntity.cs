using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Goods.Domain.Aggregation
{
    /// <summary>
    /// 商品实体
    /// </summary>
    public class GoodsEntity : AggregateRoot
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
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
        /// <summary>
        /// 售出商品
        /// </summary>
        /// <param name="number"></param>
        public void SaleGoods(int number)
        {
            if (number > Stock)
            {
                throw new DomainException($"[{Name}]库存不足,无法购买!");
            }
            if (!IsUpshelf)
            {
                throw new DomainException($"[{Name}]没有上架,无法购买!");
            }
            Stock -= number;
        }
        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="number"></param>
        public void RollbackGoods(int number)
        {
            Stock += number;
        }
        /// <summary>
        /// 修改商品库存
        /// </summary>
        /// <param name="number"></param>
        public void IncreaseGoods(int number)
        {
            Stock = number;
        }
        public void UpdateBaseInfo(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
        /// <summary>
        /// 上架/下架
        /// </summary>
        /// <param name="isUpShelf"></param>
        public void ChangeShelf(bool isUpShelf)
        {
            if (isUpShelf)
            {
                if (IsUpshelf == true)
                {
                    throw new DomainException("已上架的商品无法再次上架!");
                }
                if (Stock == 0)
                {
                    throw new DomainException("库存为0的商品无法上架!");
                }
                IsUpshelf = isUpShelf;
                UpshelfTime = DateTime.Now;
            }
            else
            {
                if (IsUpshelf == false)
                {
                    throw new DomainException("已下架的商品无法再次下架!");
                }
                IsUpshelf = isUpShelf;
            }
        }
    }
}
