using BaseServcieInterface;
using Oxygen.CsharpClientAgent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserServiceInterface.Dtos;

namespace UserServiceInterface.UseCase
{
    /// <summary>
    /// 用户登录
    /// </summary>
    [RemoteService("UserService")]
    public interface IUserLogin
    {
        Task<BaseApiResult<UserLoginResponse>> Excute(UserLoginDto input);
    }
}
