using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using InfrastructureBase;
using System.Linq.Expressions;
using Autofac;

namespace AggregateServiceManager.ServiceRoute
{
    /// <summary>
    /// 服务路由管理器
    /// </summary>
    public static class RouteManager
    {
        private static Dictionary<string, AggreServiceBase> RouteList { get; set; }
        public static void LoadAggregateServiceRoute(ILifetimeScope scope)
        {
            RouteList = new Dictionary<string, AggreServiceBase>();
            var types = typeof(RouteManager).Assembly.GetTypes().Where(x => !x.IsInterface && x.BaseType.Equals(typeof(AggreServiceBase)));
            if (types.Any())
            {
                foreach (var type in types)
                {
                    var instance = scope.Resolve(type) as AggreServiceBase;
                    var routeKey = instance.GetPropertyValue("RoutKey") as string;
                    if (!RouteList.Keys.Contains(routeKey.ToLower()))
                    {
                        RouteList.Add(routeKey.ToLower(), instance);
                    }
                }
            }
        }
        public static AggreServiceBase GetRouteService(string apiPath) => RouteList.FirstOrDefault(x => x.Key.Equals(apiPath.ToLower())).Value;
        public static List<AggreServiceBase> GetRouteServices() => RouteList.Values.ToList();
    }
    public enum CallServiceType
    {
        Aggregate = 0,
        Custom = 1
    }
}
