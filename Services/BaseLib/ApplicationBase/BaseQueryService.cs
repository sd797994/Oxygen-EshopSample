using DomainBase;
using Microsoft.Extensions.Logging;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using ApplicationBase.Infrastructure.Common;

namespace ApplicationBase
{
    public abstract class BaseQueryService<Tin, TOut> where Tin : class, new()
    {
        private readonly ILogger<BaseQueryService<Tin, TOut>> logger;
        private readonly ICurrentUserInfo currentUser;
        private readonly IGlobalTool globalTool;
        private readonly ICustomModelValidator modelValidator;
        public BaseQueryService(IIocContainer container)
        {
            logger = container.Resolve<ILogger<BaseQueryService<Tin, TOut>>>();
            currentUser = container.Resolve<ICurrentUserInfo>();
            globalTool = container.Resolve<IGlobalTool>();
            DtoExtension.BuilderTool(globalTool);
            modelValidator = container.Resolve<ICustomModelValidator>();
        }
        public abstract Task<BaseApiResult<TOut>> Query(Tin input);


        public BaseApiResult<Tout> Handle<Tout>(Tin input, Func<Tout> func)
        {
            var result = new BaseApiResult<Tout>();
            try
            {
                ValidParm(input);
                result.Data = func();
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
                    logger.LogError($"查询服务调用异常:{e.Message}\r\n调用堆栈:{e.StackTrace.ToString()}");
                    result.Code = -1;
                    result.ErrMessage = "出错了,请稍后再试";
                }
            }
            return result;
        }
        public async Task<BaseApiResult<Tout>> HandleAsync<Tout>(Tin input, Func<Task<Tout>> func)
        {
            var result = new BaseApiResult<Tout>();
            try
            {
                ValidParm(input);
                result.Data = await func();
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
                    logger.LogError($"查询服务调用异常:{e.Message}\r\n调用堆栈:{e.StackTrace.ToString()}");
                    result.Code = -1;
                    result.ErrMessage = "出错了,请稍后再试";
                }
            }
            return result;
        }
        void ValidParm(Tin input)
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
