using AggregateServiceManager.Auth;
using AggregateServiceManager.ServiceRoute;
using InfrastructureBase;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserServiceInterface.Dtos;
using UserServiceInterface.UseCase;
using ApplicationBase;
using Oxygen.IServerProxyFactory;

namespace AggregateServiceManager.User
{
    public class UserLoginAggreService : AggreServiceBase
    {
        public UserLoginAggreService(IIocContainer container) : base("/api/userservice/userlogin/excute", typeof(UserLoginDto), false, container)
        {

        }
        public override async Task<BaseApiResult<object>> Process(object input, IServerProxyFactory serverProxyFactory)
        {
            var result = await serverProxyFactory.CreateProxy<IUserLogin>().Excute((UserLoginDto)input);
            if (result.IsError())
            {
                return new BaseApiResult<object> { Code = result.Code, ErrMessage = result.ErrMessage, Data = result.Data };
            }
            else
            {
                return new BaseApiResult<object> { Data = TokenManager.CreateToken(result.Data) };
            }
        }
    }
}
