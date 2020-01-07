using ApplicationBase.Infrastructure.Common;
using Autofac;
using InfrastructureBase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trade.Infrastructure.EfDataAccess;
using TradeServiceInterface.UseCase;

namespace Trade.Host
{
    public class CustomerService: IHostedService
    {
        public CustomerService(ILifetimeScope container, IConfiguration configuration, ICacheService cacheService, TradeContext tradeContext)
        {
            IocContainer.BuilderIocContainer(container);
            cacheService.InitCacheService(configuration.GetSection("modules:2:properties:RedisConnection").Value);//启动缓存客户端
            tradeContext.Database.EnsureCreated();//自动迁移数据库
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
