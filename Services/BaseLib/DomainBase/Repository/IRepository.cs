using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DomainBase
{
    /// <summary>
    /// 基本仓储对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : Entity
    {
        /// <summary>
        /// 新增对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Add(T t);
        /// <summary>
        /// 批量新增对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool AddRange(List<T> list, bool bulkInsert = false);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Update(T t);
        /// <summary>
        /// 批量更新对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool UpdateRange(List<T> list, bool bulkUpdate = false);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Delete(T t);
        /// <summary>
        /// 根据主键获取对象
        /// </summary>
        /// <returns></returns>
        Task<T> GetAsync(object key);
        /// <summary>
        /// 根据规约获取对象
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        Task<T> GetAsync(ISpecification<T> specification, bool asNoTracking = true);
        Task<T> GetAsync(Expression<Func<T, bool>> specification, bool asNoTracking = true);
        /// <summary>
        /// 根据规约获取对象集合
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        Task<List<T>> GetManyAsync(ISpecification<T> specification, bool asNoTracking = true, params (Expression<Func<T, dynamic>>, bool)[] orderbys);
        Task<List<T>> GetManyAsync(Expression<Func<T, bool>> specification, bool asNoTracking = true, params (Expression<Func<T, dynamic>>, bool)[] orderbys);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(ISpecification<T> specification, bool asNoTracking = true);
        Task<bool> AnyAsync(Expression<Func<T, bool>> specification, bool asNoTracking = true);
        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        Task<int> CountAsync(ISpecification<T> specification, bool asNoTracking = true);
        Task<int> CountAsync(Expression<Func<T, bool>> specification, bool asNoTracking = true);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="specification"></param>
        /// <param name="orderbys"></param>
        /// <returns></returns>
        Task<PageQuery<T>> PageQueryAsync(int pageIndex, int pageSize, ISpecification<T> specification, params (Expression<Func<T, dynamic>>, bool)[] orderbys);
        Task<PageQuery<T>> PageQueryAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> specification, params (Expression<Func<T, dynamic>>, bool)[] orderbys);
        /// <summary>
        /// 工作单元提交
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAsync();
    }
}