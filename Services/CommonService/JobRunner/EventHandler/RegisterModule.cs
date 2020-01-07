using Autofac;
using Autofac.Core.Lifetime;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobRunner.EventHandler
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var requestTag = MatchingScopeLifetimeTags.RequestLifetimeScopeTag;
            var jobTag = AutofacJobActivator.LifetimeScopeTag;
            builder.RegisterAssemblyTypes(typeof(ModuleExtension).Assembly)
                .Where(type => type.GetInterface("IEventHandler") == null)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(ApplicationBase.ApplicationException).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(InfrastructureBase.InfrastructureException).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
    public static class ModuleExtension
    {
        public static ContainerBuilder RegisterBaseModule(this ContainerBuilder builder)
        {
            builder.RegisterModule(new RegisterModule());
            return builder;
        }
        public static void RegisterRunnerModule(this IServiceCollection services)
        {
            foreach (var type in typeof(ModuleExtension).Assembly.GetExportedTypes())
            {
                if (type.GetInterface("IEventHandler") != null)
                {
                    services.AddTransient(type.GetInterfaces().FirstOrDefault(), type);
                }
            }
        }
    }
}
