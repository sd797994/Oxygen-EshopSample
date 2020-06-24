using Autofac;
using BaseServcieInterface;
using Dapr.Actors;
using Dapr.Actors.Runtime;
using Oxygen.DaprActorProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationBase
{
    public abstract class BaseActor<T> : OxygenActor<T>
    {
        public BaseActor(ActorService service, ActorId actorId, ILifetimeScope container) : base(service, actorId, container) { }
        public async Task Add(T instance)
        {
            if (instance != null)
                this.instance = instance;
            await Task.CompletedTask;
        }

        public async Task<bool> Exists()
        {
            return await Task.FromResult(this.instance != null);
        }

        public async Task Delete()
        {
            if (instance != null)
                await base.DeleteInstance();
        }

        public async Task<T> Get()
        {
            return await Task.FromResult(instance);
        }
    }
}
