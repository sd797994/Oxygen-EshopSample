using AggregateServiceManager.Auth;
using ApplicationBase;
using InfrastructureBase;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BaseServcieInterface;
using Oxygen.IServerProxyFactory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AggregateServiceManager.ServiceRoute
{
    public class RouteRequestProcesser
    {
        public static async Task<object> CallService(string apiPath, string token, JObject input)
        {
            CallServiceType callType;
            Type InputType;
            var routeService = RouteManager.GetRouteService(apiPath);
            IVirtualProxyServer remoteProxy = default;
            if (routeService == null)
            {
                remoteProxy = IocContainer.Resolve<IServerProxyFactory>().CreateProxy(apiPath);
                if (remoteProxy != null)
                {
                    InputType = remoteProxy.InputType;
                    callType = CallServiceType.Custom;
                }
                else
                {
                    return new BaseApiResult<object> { ErrMessage = "创建代理失败", Code = -1 };
                }
            }
            else
            {
                InputType = routeService.InputType;
                callType = CallServiceType.Aggregate;
            }
            switch (callType)
            {
                case CallServiceType.Aggregate:
                    if (AuthManager.CheckAuth(routeService, token, input, InputType, out object aggregateInputObj))
                    {
                        return await routeService.Process(aggregateInputObj);
                    }
                    else
                    {
                        return new BaseApiResult<object> { ErrMessage = "尚未登录或登录凭证已失效,请重新登录后再试", Code = -2 };
                    }
                case CallServiceType.Custom:
                default:
                    if (AuthManager.CheckAuth(apiPath, token, input, InputType, out object customInputObj))
                    {
                        return await remoteProxy.SendAsync(customInputObj);
                    }
                    else
                    {
                        return new BaseApiResult<object> { ErrMessage = "尚未登录或登录凭证已失效,请重新登录后再试", Code = -2 };
                    }
            }
        }
    }
}
