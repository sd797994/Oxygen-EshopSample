using AggregateServiceManager.ServiceRoute;
using ApplicationBase;
using ApplicationBase.Infrastructure.Common;
using Autofac;
using InfrastructureBase;
using InfrastructureBase.EfDataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserServiceInterface.UseCase;

namespace ApiGateWay
{
    public class CustomerService: IHostedService
    {
        public CustomerService(ILifetimeScope container, IConfiguration configuration, ICacheService cacheService, DefContext defContext)
        {
            RouteManager.LoadAggregateServiceRoute(container);//初始化聚合服务路由
            cacheService.InitCacheService(configuration.GetSection("RedisConnection").Value);//启动缓存客户端
            defContext.Database.EnsureCreated();//自动迁移数据库
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
