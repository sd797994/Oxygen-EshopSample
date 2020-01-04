using DomainBase;
using Goods.Domain.Aggregation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Goods.Domain.Specification
{
    public class GoodsListQuerySpecification : ISpecification<GoodsEntity>
    {
        private string Name { get; set; }
        private bool? IsUpshelf { get; set; }
        public GoodsListQuerySpecification(string name, bool? isUpshelf)
        {
            Name = name;
            IsUpshelf = isUpshelf;
        }
        public Expression<Func<GoodsEntity, bool>> SatisfiedBy()
        {
            var where = PredicateBuilder.True<GoodsEntity>();
            if (!string.IsNullOrEmpty(Name))
            {
                where = where.And(x => x.Name.Contains(Name));
            }
            if (IsUpshelf != null)
            {
                where = where.And(x => x.IsUpshelf == IsUpshelf);
            }
            return where;
        }
    }
}
