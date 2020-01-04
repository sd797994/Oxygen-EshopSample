using DomainBase;
using Microsoft.Extensions.Logging;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationBase
{
    public abstract class BaseUseCase<T> where T : class, new()
    {
        private readonly ILogger<BaseUseCase<T>> logger;
        private readonly ICurrentUserInfo currentUser;
        private readonly ICustomModelValidator modelValidator;
        public BaseUseCase(IIocContainer container)
        {
            logger = container.Resolve<ILogger<BaseUseCase<T>>>();
            currentUser = container.Resolve<ICurrentUserInfo>();
            modelValidator = container.Resolve<ICustomModelValidator>();
        }
        public BaseApiResult<Tout> Handle<Tout>(T input, Func<Tout> func, Func<Exception, Task> catchfunc = null)
        {
            var result = new BaseApiResult<Tout>();
            try
            {
                ValidParm(input);
                result.Data = func();
                result.Code = 0;
            }
            catch (Exception e)
            {
                if(e is CustomerException)
                {
                    result.Code = ((CustomerException)e).Code;
                    result.ErrMessage = e.Message;
                }
                else
                {
                    logger.LogError(e.Message);
                    result.Code = -1;
                    result.ErrMessage = "出错了,请稍后再试";
                }
                try
                {
                    catchfunc?.Invoke(e);
                }
                catch (Exception) { }
            }
            return result;
        }
        public async Task<BaseApiResult<Tout>> HandleAsync<Tout>(T input, Func<Task<Tout>> func, Func<Exception, Task> catchfunc = null)
        {
            var result = new BaseApiResult<Tout>();
            try
            {
                ValidParm(input);
                result.Data = await func();
                result.Code = 0;
            }
            catch (Exception e)
            {
                if (e is CustomerException)
                {
                    result.Code = ((CustomerException)e).Code;
                    result.ErrMessage = e.Message;
                }
                else
                {
                    logger.LogError(e.Message);
                    result.Code = -1;
                    result.ErrMessage = "出错了,请稍后再试";
                }
                try
                {
                    catchfunc?.Invoke(e);
                }
                catch (Exception) { }
            }
            return result;
        }
        void ValidParm(T input)
        {
            if (input is BaseAuthDto)
            {
                var AuthInfo = input as BaseAuthDto;
                currentUser.Account = AuthInfo.Account;
                currentUser.UserId = AuthInfo.UserId;
            }
            var validResults = modelValidator.Valid(input);
            if (validResults.Count > 0)
            {
                throw new ApplicationException(validResults[0].ErrorMessage);
            }
        }
    }
}
