using ApplicationBase;
using BaseServcieInterface;
using DomainBase;
using InfrastructureBase;
using Microsoft.Extensions.Logging;
using Oxygen.IServerProxyFactory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AggregateServiceManager.ServiceRoute
{
    public abstract class AggreServiceBase
    {
        private readonly ILogger<AggreServiceBase> logger;
        public AggreServiceBase(string routeKey, Type inputType, bool needAuthCheck = false, IIocContainer container = null)
        {
            RoutKey = routeKey;
            InputType = inputType;
            NeedAuthCheck = needAuthCheck;
            logger = container.Resolve<ILogger<AggreServiceBase>>();
        }
        public string RoutKey { get; }
        public Type InputType { get; set; }
        public bool NeedAuthCheck { get; set; }
        public abstract Task<BaseApiResult<object>> Process(object input, IServerProxyFactory serverProxyFactory);

        public async Task<BaseApiResult<object>> HandleAsync<Tout>(Func<Task<Tout>> func, Func<CustomerException, Task> catchfunc = null)
        {
            var result = new BaseApiResult<object>();
            try
            {
                result.Data = await func();
                result.Code = 0;
            }
            catch (Exception e)
            {
                try
                {
                    await catchfunc(e as CustomerException);
                }
                finally
                {
                    if (e is CustomerException)
                    {
                        result.Code = ((CustomerException)e).Code;
                        result.ErrMessage = e.Message;
                    }
                    else
                    {
                        logger.LogError($"聚合服务调用异常:{e.Message}\r\n调用堆栈:{e.StackTrace.ToString()}");
                        result.Code = -1;
                        result.ErrMessage = "出错了,请稍后再试";
                    }
                }
            }
            return result;
        }
    }
}
