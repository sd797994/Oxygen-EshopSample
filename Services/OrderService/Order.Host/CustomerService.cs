using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OrderServiceInterface.UseCase;
using Autofac;
using InfrastructureBase;
using Microsoft.Extensions.Configuration;
using ApplicationBase.Infrastructure.Common;
using Order.Infrastructure.EfDataAccess;

namespace Order.Host
{
    public class CustomerService: IHostedService
    {
        public CustomerService(ILifetimeScope container, IConfiguration configuration, ICacheService cacheService, OrderContext orderContext)
        {
            cacheService.InitCacheService(configuration.GetSection("modules:2:properties:RedisConnection").Value);//启动缓存客户端
            orderContext.Database.EnsureCreated();//自动迁移数据库
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
