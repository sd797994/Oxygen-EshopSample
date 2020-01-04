using DomainBase;
using Microsoft.Extensions.Logging;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationBase
{
    public abstract class BaseEventHandler<T>
    {
        private readonly ILogger<BaseEventHandler<T>> logger;
        private readonly ICurrentUserInfo currentUser;
        public BaseEventHandler(IIocContainer container) { this.logger = container.Resolve<ILogger<BaseEventHandler<T>>>(); this.currentUser = container.Resolve<ICurrentUserInfo>(); }
        public abstract Task Handle(T input);
        public void Handle(T input, Action func, Action<Exception> catchfunc = null)
        {
            try
            {
                AddAuthParm(input);
                func();
            }
            catch (Exception e)
            {
                try
                {
                    catchfunc?.Invoke(e);
                }
                catch (Exception) { }
                logger.LogError(e.Message);
            }
        }
        public async Task HandleAsync(T input,Func<Task> func, Func<Exception, Task> catchfunc = null)
        {
            try
            {
                AddAuthParm(input);
                await func();
            }
            catch (Exception e)
            {
                try
                {
                    catchfunc?.Invoke(e);
                }
                catch (Exception) { }
                logger.LogError(e.Message);
            }
        }
        void AddAuthParm(T input)
        {
            if (input is BaseAuthDto)
            {
                var AuthInfo = input as BaseAuthDto;
                currentUser.Account = AuthInfo.Account;
                currentUser.UserId = AuthInfo.UserId;
            }
        }
    }
}
