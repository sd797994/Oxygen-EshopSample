using System;
using System.Threading.Tasks;
using AggregateServiceManager.ServiceRoute;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using BaseServcieInterface;
using Oxygen.IServerProxyFactory;
using Microsoft.Extensions.Logging;
using InfrastructureBase;
using GoodsServiceInterface.Actor;
using Oxygen.DaprActorProvider;

namespace ApiGateWay.Controllers
{
    [Route("api/{*service}")]
    [ApiController]
    public class EshopApiGateWayController : ControllerBase
    {
        private readonly IServerProxyFactory serverProxyFactory;
        private readonly ILogger<EshopApiGateWayController> logger;
        public EshopApiGateWayController(IServerProxyFactory serverProxyFactory, ILogger<EshopApiGateWayController> logger)
        {
            this.serverProxyFactory = serverProxyFactory;
            this.logger = logger;
        }
        // GET api/values
        [HttpPost]
        public async Task<object> Invoke(JObject input)
        {
            if (input != null)
            {
                try
                {
                    return await RouteRequestProcesser.CallService(serverProxyFactory, Request.Path, Request.Headers["Token"], input);
                }
                catch (Exception e)
                {
                    logger.LogError($"网关调用异常:{e.Message}\r\n调用堆栈:{e.StackTrace.ToString()}");
                    return new BaseApiResult<object> { ErrMessage = "出错了,请稍后再试", Code = -1 };
                }
            }
            return new BaseApiResult<object> { ErrMessage = "无返回值", Code = -1 };
        }
    }
}