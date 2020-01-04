using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AggregateServiceManager.ServiceRoute;
using ApplicationBase;
using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BaseServcieInterface;
using Oxygen.CommonTool;
using Oxygen.IServerProxyFactory;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ApiGateWay.Controllers
{
    [Route("api/{*service}")]
    [ApiController]
    public class EshopApiGateWayController : ControllerBase
    {
        public EshopApiGateWayController()
        {

        }
        // GET api/values
        [HttpPost]
        public async Task<IActionResult> Invoke(JObject input)
        {
            if (input != null)
            {
                try
                {
                    return new JsonResult(await RouteRequestProcesser.CallService(Request.Path, Request.Headers["Token"], input));
                }
                catch(Exception e)
                {
                    return new JsonResult(new BaseApiResult<object> { ErrMessage = "出错了,请稍后再试", Code = -1 });
                }
            }
            return new JsonResult(new BaseApiResult<object> { ErrMessage = "无返回值", Code = -1 });
        }
    }
}