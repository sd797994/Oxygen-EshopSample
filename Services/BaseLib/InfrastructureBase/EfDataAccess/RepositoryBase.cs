using ApplicationBase;
using DomainBase;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InfrastructureBase
{
    public enum OrderEnum
    {
        OrderBy,
        ThenBy,
        OrderByDescending,
        ThenByDescending,
    }
    public abstract class RepositoryBase<TDo, TPo, TContext> : IRepository<TDo> where TDo : Entity, new() where TPo : PersistenceObjectBase, new() where TContext : DbContext
    {
        private readonly TContext _context;
        private ICurrentUserInfo currentUser;
        protected RepositoryBase(TContext context, ICurrentUserInfo currentUser)
        {
            _context = context;
            this.currentUser = currentUser;
        }
        /// <summary>
        /// 新增对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool Add(TDo t)
        {
            var po = GetPersistentObject(t, true);
            _context.Set<TPo>().Add(po);
            return true;
        }
        public virtual bool AddRange(List<TDo> list, bool bulkInsert = false)
        {
            var poList = GetPersistentObjectList(list);
            if (bulkInsert)
                _context.BulkInsert(poList);
            else
                _context.Set<TPo>().AddRange(poList);
            return true;
        }
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool Update(TDo t)
        {
            var po = GetPersistentObject(t);
            _context.Set<TPo>().Attach(po).State = EntityState.Modified;
            _context.Entry(po).NotModifyBaseProperties();
            return true;
        }
        public virtual bool UpdateRange(List<TDo> list, bool bulkUpdate = false)
        {
            if (bulkUpdate)
                _context.BulkUpdate(GetPersistentObjectList(list));
            else
                list.ForEach(x => Update(x));
            return true;
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool Delete(TDo t)
        {
            _context.Set<TPo>().Remove(GetPersistentObject(t));
            return true;
        }

        /// <summary>
        /// 根据主键获取对象
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TDo> GetAsync(object key)
        {
            var result = await _context.Set<TPo>().FindAsync(key);
            if (result != null)
            {
                _context.Set<TPo>().Attach(result).State = EntityState.Detached;
                return GetDomainEntity(result);
            }
            return null;
        }
        /// <summary>
        /// 根据条件查询对象
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        public virtual async Task<TDo> GetAsync(Expression<Func<TDo, bool>> specification, bool asNoTracking = true)
        {
            return GetDomainEntity(await GetAll(asNoTracking).FirstOrDefaultAsync(specification.ReplaceParameter<TDo, TPo>()));
        }
        public virtual async Task<TDo> GetAsync(ISpecification<TDo> specification, bool asNoTracking = true)
        {
            return GetDomainEntity(await GetAll(asNoTracking).FirstOrDefaultAsync(specification.SatisfiedBy().ReplaceParameter<TDo, TPo>()));
        }
        /// <summary>
        /// 根据条件查询对象集合
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        public virtual async Task<List<TDo>> GetManyAsync(ISpecification<TDo> specification, bool asNoTracking = true, params (Expression<Func<TDo, dynamic>>, bool)[] orderbys)
        {
            if (orderbys != null && orderbys.Any())
            {
                var query = GetOrderQueryable(GetAll(asNoTracking, !orderbys.Any()).Where(specification.SatisfiedBy().ReplaceParameter<TDo, TPo>()), orderbys.ToList());
                return GetDomainEntityList(await query.ToListAsync());
            }
            else
            {
                return GetDomainEntityList(await GetAll(asNoTracking).Where(specification.SatisfiedBy().ReplaceParameter<TDo, TPo>()).ToListAsync());
            }
        }
        public virtual async Task<List<TDo>> GetManyAsync(Expression<Func<TDo, bool>> specification, bool asNoTracking = true, params (Expression<Func<TDo, dynamic>>, bool)[] orderbys)
        {
            if (orderbys != null && orderbys.Any())
            {
                var query = GetOrderQueryable(GetAll(asNoTracking, !orderbys.Any()).Where(specification.ReplaceParameter<TDo, TPo>()), orderbys.ToList());
                return GetDomainEntityList(await query.ToListAsync());
            }
            else
            {
                return GetDomainEntityList(await GetAll(asNoTracking, !orderbys.Any()).Where(specification.ReplaceParameter<TDo, TPo>()).ToListAsync());
            }
        }
        /// <summary>
        /// 根据条件判断对象是否存在
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<TDo, bool>> specification, bool asNoTracking = true)
        {
            return await GetAll(asNoTracking).AnyAsync(specification.ReplaceParameter<TDo, TPo>());
        }
        public virtual async Task<bool> AnyAsync(ISpecification<TDo> specification, bool asNoTracking = true)
        {
            return await GetAll(asNoTracking).AnyAsync(specification.SatisfiedBy().ReplaceParameter<TDo, TPo>());
        }

        /// <summary>
        /// 根据条件返回对象数量
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<TDo, bool>> specification, bool asNoTracking = true)
        {
            return await GetAll(asNoTracking).CountAsync(specification.ReplaceParameter<TDo, TPo>());
        }
        public virtual async Task<int> CountAsync(ISpecification<TDo> specification, bool asNoTracking = true)
        {
            return await GetAll(asNoTracking).CountAsync(specification.SatisfiedBy().ReplaceParameter<TDo, TPo>());
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="specification"></param>
        /// <param name="orderbys"></param>
        /// <returns></returns>
        public virtual async Task<PageQuery<TDo>> PageQueryAsync(int pageIndex, int pageSize, ISpecification<TDo> specification, params (Expression<Func<TDo, dynamic>>, bool)[] orderbys)
        {
            var query = GetAll(true, !orderbys.Any()).Where(specification.SatisfiedBy().ReplaceParameter<TDo, TPo>());
            var result = new PageQuery<TDo>();
            result.PageIndex = pageIndex;
            result.Total = await query.CountAsync();
            if (result.Total > 0)
            {
                result.Data = GetDomainEntityList(await PageQueryResult(query, pageIndex, pageSize, orderbys));
            }
            else
            {
                result.Data = new List<TDo>();
            }
            return result;
        }
        public virtual async Task<PageQuery<TDo>> PageQueryAsync(int pageIndex, int pageSize, Expression<Func<TDo, bool>> specification, params (Expression<Func<TDo, dynamic>>, bool)[] orderbys)
        {
            var query = GetAll(true, !orderbys.Any()).Where(specification.ReplaceParameter<TDo, TPo>());
            var result = new PageQuery<TDo>();
            result.PageIndex = pageIndex;
            result.Total = await query.CountAsync();
            if (result.Total > 0)
            {
                result.Data = GetDomainEntityList(await PageQueryResult(query, pageIndex, pageSize, orderbys));
            }
            else
            {
                result.Data = new List<TDo>();
            }
            return result;
        }
        /// <summary>
        /// 异步工作单元
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> SaveAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }
        #region base methods

        /// <summary>
        /// 强制重写转换类用于查询
        /// </summary>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        public IQueryable<TPo> GetAll(bool asNoTracking = true, bool defaultOrderBy = false)
        {
            if (!asNoTracking)
            {
                if (!defaultOrderBy)
                {
                    return _context.Set<TPo>().Where(x => !x.IsDeleted);
                }
                else
                {
                    return _context.Set<TPo>().Where(x => !x.IsDeleted).OrderByDescending(x => x.CreateTime);
                }
            }
            else
            {
                if (!defaultOrderBy)
                {
                    return _context.Set<TPo>().AsNoTracking().Where(x => !x.IsDeleted);
                }
                else
                {
                    return _context.Set<TPo>().AsNoTracking().Where(x => !x.IsDeleted).OrderByDescending(x => x.CreateTime);
                }
            }
        }
        async Task<List<TPo>> PageQueryResult(IQueryable<TPo> query, int pageIndex, int pageSize, params (Expression<Func<TDo, dynamic>>, bool)[] orderbys)
        {
            if (orderbys != null && orderbys.Any())
            {
                query = GetOrderQueryable(query, orderbys.ToList());
            }
            return await (query.Skip((pageIndex - 1) * pageSize).Take(pageSize)).ToListAsync();
        }
        IOrderedQueryable<TPo> GetOrderQueryable(IQueryable<TPo> source, List<(Expression<Func<TDo, dynamic>>, bool)> orderbyList)
        {
            orderbyList.ForEach(x =>
            {
                var newOrder = x.Item1.ReplaceParameter<TDo, TPo>();
                source = (IQueryable<TPo>)source.Provider.CreateQuery(Expression.Call(typeof(Queryable),
                    orderbyList.IndexOf(x) == 0 && x.Item2 ?
                    nameof(OrderEnum.OrderBy) :
                    orderbyList.IndexOf(x) == 0 && !x.Item2 ?
                    nameof(OrderEnum.OrderByDescending) :
                    orderbyList.IndexOf(x) > 0 && x.Item2 ?
                    nameof(OrderEnum.ThenBy) : nameof(OrderEnum.ThenByDescending)
                    , new[] { typeof(TPo), newOrder.Body.Type }, source.Expression, newOrder));
            });
            return (IOrderedQueryable<TPo>)source;
        }


        public TPo GetPersistentObject(TDo source, bool addCreateUserId = false)
        {
            var result = IocContainer.Resolve<IGlobalTool>().Map<TDo, TPo>(source);
            if (addCreateUserId)
                result.CreateUserId = currentUser.UserId;
            return result;
        }
        public List<TPo> GetPersistentObjectList(ICollection<TDo> source, bool addCreateUserId = false)
        {
            var result = IocContainer.Resolve<IGlobalTool>().MapList<TDo, TPo>(source);
            if (addCreateUserId)
                result.ForEach(x => x.CreateUserId = currentUser.UserId);
            return result;
        }
        public static TDo GetDomainEntity(TPo source)
        {
            return IocContainer.Resolve<IGlobalTool>().Map<TPo, TDo>(source);
        }
        public static List<TDo> GetDomainEntityList(ICollection<TPo> source)
        {
            return IocContainer.Resolve<IGlobalTool>().MapList<TPo, TDo>(source);
        }
        public TOut GetPersistentObject<TIn, TOut>(TIn source, bool addCreateUserId = false) where TIn : Entity, new() where TOut : PersistenceObjectBase
        {
            var result = IocContainer.Resolve<IGlobalTool>().Map<TIn, TOut>(source);
            if (addCreateUserId)
                result.CreateUserId = currentUser.UserId;
            return result;
        }
        public List<TOut> GetPersistentObjectList<TIn, TOut>(ICollection<TIn> source, bool addCreateUserId = false) where TIn : Entity, new() where TOut : PersistenceObjectBase
        {
            var result = IocContainer.Resolve<IGlobalTool>().MapList<TIn, TOut>(source);
            if (addCreateUserId)
                result.ForEach(x => x.CreateUserId = currentUser.UserId);
            return result;
        }
        public static TOut GetDomainEntity<TIn, TOut>(TIn source) where TIn : PersistenceObjectBase, new() where TOut : Entity
        {
            return IocContainer.Resolve<IGlobalTool>().Map<TIn, TOut>(source);
        }
        public static List<TOut> GetDomainEntityList<TIn, TOut>(ICollection<TIn> source) where TIn : PersistenceObjectBase, new() where TOut : Entity
        {
            return IocContainer.Resolve<IGlobalTool>().MapList<TIn, TOut>(source);
        }
        #endregion
    }
}
