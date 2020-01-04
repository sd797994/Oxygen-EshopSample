using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DomainBase
{
    public interface ISpecification<TEntity>
        where TEntity : class
    {
        Expression<Func<TEntity, bool>> SatisfiedBy();
    }
    public interface IOperateSpecification<TEntity>
    {
        Task<bool> SatisfiedBy(TEntity T);
    }
}
