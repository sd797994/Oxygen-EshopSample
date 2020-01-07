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
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserServiceInterface.UseCase;
namespace JobRunner
{
    public class CustomerService: IHostedService
    {
        public CustomerService(DefContext defContext)
        {
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
