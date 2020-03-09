﻿using ApplicationBase;
using ApplicationBase.Infrastructure.Common;
using Autofac;
using InfrastructureBase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using User.Infrastructure.EfDataAccess;
using UserServiceInterface.UseCase;

namespace User.Host
{
    public class CustomerService: IHostedService
    {
        public CustomerService(ILifetimeScope container, IConfiguration configuration, ICacheService cacheService, UserContext userContext)
        {
            cacheService.InitCacheService(configuration.GetSection("modules:2:properties:RedisConnection").Value);//启动缓存客户端
            userContext.Database.EnsureCreated();//自动迁移数据库
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
