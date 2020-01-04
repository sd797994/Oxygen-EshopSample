using Goods.Domain.Aggregation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Goods.Domain.Factory
{
    public class GoodsFactory
    {
        public GoodsEntity Create(string name, decimal price,string imageId, int stock)
        {
            return new GoodsEntity() { Name = name, Stock = stock, Price = price,ImageId= imageId, Id = Guid.NewGuid() };
        }
    }
}
