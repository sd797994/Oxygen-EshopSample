using ApplicationBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace InfrastructureBase
{
	public static class ObjectExtensions
    {
        public static object GetPropertyValue<T>(this T t, string name)
        {
            Type type = t.GetType();
            PropertyInfo p = type.GetProperty(name);
            if (p == null)
            {
                return null;
            }
            ParameterExpression param_obj = Expression.Parameter(typeof(T));
            UnaryExpression body;
            if (typeof(T).BaseType == null)
            {
                body = Expression.Convert(Expression.Property(Expression.Convert(param_obj, type), name), typeof(object));
            }
            else
            {
                body = Expression.Convert(Expression.Property(param_obj, p), typeof(object));
            }
            var result = Expression.Lambda<Func<T, object>>(body, param_obj);
            var getValue = result.Compile();
            return getValue(t);
        }

        public static T CreateInstacne<T>(this Type type)
        {
            NewExpression newExp = Expression.New(type);
            Expression<Func<T>> lambdaExp =
                Expression.Lambda<Func<T>>(newExp, null);
            Func<T> func = lambdaExp.Compile();
            return func();
        }

        public static TOut Mapper<TIn,TOut>(this TIn source) where TIn : class where TOut : class
        {
            return MapperExtension.Mapper<TIn, TOut>.Map(source);
        }
        public static List<TOut> MapperList<TIn, TOut>(this ICollection<TIn> source) where TIn : class where TOut : class
        {
            return MapperExtension.Mapper<TIn, TOut>.MapList(source);
        }
    }
}
