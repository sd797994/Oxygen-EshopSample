using BaseServcieInterface;
using Oxygen.CsharpClientAgent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserServiceInterface.Dtos;

namespace UserServiceInterface.Query
{
    [AuthSign]
    [RemoteService("UserService")]
    public interface IGetUserState
    {
        Task<BaseApiResult<UserLoginResponse>> Query(BaseAuthDto input);
    }
}
