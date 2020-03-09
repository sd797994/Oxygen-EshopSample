using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateWay
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> logger;
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            this.logger = logger;
        }
        public void OnException(ExceptionContext e)
        {
            logger.LogError($"网关捕获全局异常:{e.Exception.Message}\r\n调用堆栈:{e.Exception.StackTrace.ToString()}");
        }
    }
}
