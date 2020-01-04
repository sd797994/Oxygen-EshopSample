using Autofac;
using Autofac.Core.Lifetime;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AggregateServiceManager
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var requestTag = MatchingScopeLifetimeTags.RequestLifetimeScopeTag;
            builder.RegisterAssemblyTypes(typeof(ModuleExtension).Assembly)
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
    }
}
