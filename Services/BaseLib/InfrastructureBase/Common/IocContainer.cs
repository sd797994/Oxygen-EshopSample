using ApplicationBase;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureBase
{
    public class _IocContainer : IIocContainer
    {
        private ILifetimeScope _container;
        public _IocContainer(ILifetimeScope container)
        {
            _container = container;
        }
        public T Resolve<T>()
        {
            try
            {
                if (_container == null)
                {
                    throw new Exception("IOC实例化出错!");
                }
                return _container.Resolve<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public object Resolve(Type type)
        {
            try
            {
                if (_container == null)
                {
                    throw new Exception("IOC实例化出错!");
                }
                return _container.Resolve(type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public static class IocContainer
    {
        private static ILifetimeScope _container;
        public static void BuilderIocContainer(ILifetimeScope container)
        {
            _container = container;
        }

        public static T Resolve<T>()
        {
            try
            {
                if (_container == null)
                {
                    throw new Exception("IOC实例化出错!");
                }
                return _container.Resolve<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object Resolve(Type type)
        {
            try
            {
                if (_container == null)
                {
                    throw new Exception("IOC实例化出错!");
                }
                return _container.Resolve(type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
