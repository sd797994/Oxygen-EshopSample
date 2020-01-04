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
    /// 用户创建用例
    /// </summary>
    [RemoteService("UserService")]
    public interface IUserAccountCreate
    {
        Task<BaseApiResult<bool>> Excute(AccountCreateDto input);
    }
}
