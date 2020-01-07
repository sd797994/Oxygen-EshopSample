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

namespace AggregateServiceManager.User
{
    public class UserLoginAggreService : AggreServiceBase
    {
        public UserLoginAggreService() : base("/api/userservice/userlogin/excute", typeof(UserLoginDto))
        {

        }
        public override async Task<BaseApiResult<object>> Process(object input)
        {
            var userLogin = IocContainer.Resolve<IUserLogin>();
            var result = await userLogin.Excute((UserLoginDto)input);
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
