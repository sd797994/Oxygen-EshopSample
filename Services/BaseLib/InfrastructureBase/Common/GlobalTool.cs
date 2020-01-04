using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq.Expressions;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using ApplicationBase;
using BaseServcieInterface;
using System.ComponentModel.DataAnnotations;

namespace InfrastructureBase
{
    public class GlobalTool: IGlobalTool
    {
        /// <summary>
        /// 获取MD5
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string GetMD5SaltCode(string origin, string salt)
        {
            using (var md5 = MD5.Create())
            {
               return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(origin + salt))).Replace("-", "");
            }
        }
        /// <summary>
        /// 将对象TSource转换为TTarget
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public TTarget Map<TSource, TTarget>(TSource source) where TSource : class where TTarget : class
        {
            return MapperExtension.Mapper<TSource, TTarget>.Map(source);
        }

        public List<TTarget> MapList<TSource, TTarget>(IEnumerable<TSource> sources) where TSource : class where TTarget : class
        {
            return MapperExtension.Mapper<TSource, TTarget>.MapList(sources);
        }
    }
}
