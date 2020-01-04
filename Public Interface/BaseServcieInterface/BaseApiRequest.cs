using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BaseServcieInterface
{
    public class PageQueryReq
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class OrderQueryReq
    {
        public List<OrderQueryDetailReq> OrderParms { get; set; }
    }
    public class PageOrderQueryReq : PageQueryReq
    {
        public List<OrderQueryDetailReq> OrderParms { get; set; }
    }
    public class PageQueryOuthReq : BaseAuthDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class OrderQueryOuthReq : BaseAuthDto
    {
        public List<OrderQueryDetailReq> OrderParms { get; set; }
    }
    public class PageOrderQueryOuthReq : PageQueryOuthReq
    {
        public List<OrderQueryDetailReq> OrderParms { get; set; }
    }
    public class PageQueryDto<T>
    {
        public int PageIndex { get; set; }
        public int Total { get; set; }
        public List<T> Data { get; set; }
    }

    public class OrderQueryDetailReq
    {
        public string OrderName { get; set; }
        public bool IsAsc { get; set; }
    }
    public static class OrderQueryDetailReqExtension
    {
        public static (Expression<Func<T, dynamic>>, bool)[] ConvertToOrderExpression<T>(this List<OrderQueryDetailReq> coures)
        {
            var orderList = new List<(Expression<Func<T, dynamic>>, bool)>();
            var sourceType = typeof(T);
            var properties = sourceType.GetProperties();
            coures.ForEach(orderItem =>
            {
                var type = properties.FirstOrDefault(x => x.Name.ToLower() == orderItem.OrderName.ToLower());
                if (type != null)
                {
                    var parameterExpression = Expression.Parameter(sourceType, "p");
                    var property = Expression.Property(parameterExpression, type);
                    Expression conversion = Expression.Convert(property, typeof(object));
                    var lambda = Expression.Lambda<Func<T, dynamic>>(conversion, parameterExpression);
                    orderList.Add((lambda, orderItem.IsAsc));
                }
            });
            return orderList.ToArray();
        }
    }
}
