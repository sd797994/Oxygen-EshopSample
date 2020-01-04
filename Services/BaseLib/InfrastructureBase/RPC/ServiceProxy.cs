using ApplicationBase;
using Oxygen.IServerProxyFactory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureBase
{
    public class ServiceProxy : IServiceProxy
    {
        private readonly IServerProxyFactory serverProxyFactory;
        public ServiceProxy(IServerProxyFactory serverProxyFactory)
        {
            this.serverProxyFactory = serverProxyFactory;
        }

        public T CreateProxy<T>() where T : class
        {
            return serverProxyFactory.CreateProxy<T>();
        }
    }
}
