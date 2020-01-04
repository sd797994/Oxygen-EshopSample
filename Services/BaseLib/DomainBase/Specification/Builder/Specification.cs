using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace DomainBase
{
    public class Specification<TEntity> : ISpecification<TEntity>
        where TEntity : class
    {
        #region Members

        Expression<Func<TEntity, bool>> _MatchingCriteria;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for Direct Specification
        /// </summary>
        /// <param name="matchingCriteria">A Matching Criteria</param>
        public Specification(Expression<Func<TEntity, bool>> matchingCriteria)
        {
            if (matchingCriteria == (Expression<Func<TEntity, bool>>)null)
                throw new ArgumentNullException("matchingCriteria");

            _MatchingCriteria = matchingCriteria;
        }

        #endregion

        #region Override

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return _MatchingCriteria;
        }
        #endregion
    }
}
