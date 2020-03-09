using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Oxygen.CommonTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateWay
{
    public class GlobalRequestFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                builder.UseMiddleware<RequestMiddleware>();
                next(builder);
            };
        }
        public class RequestMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILifetimeScope container;
            public RequestMiddleware(
                RequestDelegate next, ILifetimeScope container)
            {
                _next = next;
                this.container = container;
            }

            public async Task Invoke(HttpContext httpContext)
            {
                //每次请求将重新初始化全局容器确保容器唯一
                OxygenIocContainer.BuilderIocContainer(container);
                //为客户端信息添加追踪头
                OxygenIocContainer.Resolve<CustomerInfo>().SetTraceHeader(TraceHeaderHelper.GetTraceHeaders(httpContext.Request.Headers));
                await _next(httpContext);
            }
        }
    }
}
