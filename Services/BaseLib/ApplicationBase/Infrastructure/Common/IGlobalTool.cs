using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationBase
{
    /// <summary>
    /// 通用工具
    /// </summary>
    public interface IGlobalTool
    {
        /// <summary>
        /// 加盐生成MD5
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        string GetMD5SaltCode(string origin, string salt);
        TTarget Map<TSource, TTarget>(TSource source) where TSource : class where TTarget : class;
        List<TTarget> MapList<TSource, TTarget>(IEnumerable<TSource> sources) where TSource : class where TTarget : class;
    }
}
