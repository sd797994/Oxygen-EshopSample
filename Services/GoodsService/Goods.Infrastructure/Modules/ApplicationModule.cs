using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Goods.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Goods.Infrastructure.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ApplicationIocModel).Assembly)
                .Where(type => type.GetInterface("IEventHandler") == null)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(ApplicationBase.ApplicationException).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }

    public static class ModuleExtension
    {
        public static void RegisterCapSubscribe(this IServiceCollection services)
        {
            foreach (var type in typeof(ApplicationIocModel).Assembly.GetExportedTypes())
            {
                if (type.GetInterface("IEventHandler") != null)
                {
                    services.AddTransient(type.GetInterfaces().FirstOrDefault(), type);
                }
            }
        }
    }
}
