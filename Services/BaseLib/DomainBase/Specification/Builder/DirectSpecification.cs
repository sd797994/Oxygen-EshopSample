using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DomainBase
{
    public sealed class DirectSpecification<TEntity>
        : ISpecification<TEntity>
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
        public DirectSpecification(Expression<Func<TEntity, bool>> matchingCriteria)
        {
            if (matchingCriteria == null)
                throw new ArgumentNullException("matchingCriteria");

            _MatchingCriteria = matchingCriteria;
        }

        #endregion

        #region Override

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return _MatchingCriteria;
        }
        #endregion
    }
}
